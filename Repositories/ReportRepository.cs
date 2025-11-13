using Banking_Payments.Context;
using Banking_Payments.Models;
using Banking_Payments.Models.DTO.Reports;
using Banking_Payments.Models.Enums;
using Banking_Payments.Services;
using Google;
using Microsoft.EntityFrameworkCore;

namespace Banking_Payments.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ReportRepository> _logger;

        public ReportRepository(AppDbContext context, ILogger<ReportRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // ==================== SUPER ADMIN REPORTS ====================

        public async Task<SystemOverviewReportDTO> GetSystemOverviewAsync()
        {
            var totalBanks = await _context.Banks.CountAsync();
            var activeBanks = await _context.Banks.CountAsync(b => b.IsActive);
            var inactiveBanks = totalBanks - activeBanks;

            var totalClients = await _context.Clients.CountAsync();
            var totalBankUsers = await _context.BankUsers.CountAsync();
            var totalEmployees = await _context.Employees.CountAsync();
            var totalBeneficiaries = await _context.Beneficiaries.CountAsync();

            var totalPayments = await _context.Payments.CountAsync();
            var pendingPayments = await _context.Payments.CountAsync(p => p.status == VerificationStatus.Pending);
            var approvedPayments = await _context.Payments.CountAsync(p => p.status == VerificationStatus.Verified);
            var rejectedPayments = await _context.Payments.CountAsync(p => p.status == VerificationStatus.Rejected);
            var totalPaymentValue = await _context.Payments
                .Where(p => p.status == VerificationStatus.Verified)
                .SumAsync(p => p.Amount);

            var totalSalaryDisbursements = await _context.SalaryDisbursements.CountAsync();
            var totalSalaryValue = await _context.SalaryDisbursements.SumAsync(s => s.Amount);

            var pendingVerifications = await _context.Clients.CountAsync(c => c.VerificationStatus == "Pending");
            var verifiedClients = await _context.Clients.CountAsync(c => c.VerificationStatus == "Verified");
            var rejectedClients = await _context.Clients.CountAsync(c => c.VerificationStatus == "Rejected");

            return new SystemOverviewReportDTO
            {
                TotalBanks = totalBanks,
                ActiveBanks = activeBanks,
                InactiveBanks = inactiveBanks,
                TotalClients = totalClients,
                TotalBankUsers = totalBankUsers,
                TotalEmployees = totalEmployees,
                TotalBeneficiaries = totalBeneficiaries,
                TotalPayments = totalPayments,
                PendingPayments = pendingPayments,
                ApprovedPayments = approvedPayments,
                RejectedPayments = rejectedPayments,
                TotalPaymentValue = totalPaymentValue,
                TotalSalaryDisbursements = totalSalaryDisbursements,
                TotalSalaryValue = totalSalaryValue,
                PendingVerifications = pendingVerifications,
                VerifiedClients = verifiedClients,
                RejectedClients = rejectedClients,
                GeneratedAt = DateTime.UtcNow
            };
        }

        public async Task<IEnumerable<BankPerformanceReportDTO>> GetBankPerformanceReportAsync(DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Banks
                .Include(b => b.Clients)
                .Include(b => b.BankUsers)
                .AsQueryable();

            var banks = await query.ToListAsync();
            var reports = new List<BankPerformanceReportDTO>();

            foreach (var bank in banks)
            {
                var clientQuery = _context.Clients.Where(c => c.BankId == bank.BankId);
                var paymentQuery = _context.Payments.Where(p => p.Client.BankId == bank.BankId);
                var documentQuery = _context.Documents.Where(d => d.Client.BankId == bank.BankId);

                if (startDate.HasValue)
                {
                    clientQuery = clientQuery.Where(c => c.CreatedAt >= startDate.Value);
                    paymentQuery = paymentQuery.Where(p => p.PaymentDate >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    clientQuery = clientQuery.Where(c => c.CreatedAt <= endDate.Value);
                    paymentQuery = paymentQuery.Where(p => p.PaymentDate <= endDate.Value);
                }

                var totalClients = await clientQuery.CountAsync();
                var activeClients = await clientQuery.CountAsync(c => c.IsActive);
                var inactiveClients = totalClients - activeClients;

                var totalPayments = await paymentQuery.CountAsync();
                var pendingPayments = await paymentQuery.CountAsync(p => p.status == VerificationStatus.Pending);
                var approvedPayments = await paymentQuery.CountAsync(p => p.status == VerificationStatus.Verified);
                var rejectedPayments = await paymentQuery.CountAsync(p => p.status == VerificationStatus.Rejected);
                var totalPaymentValue = await paymentQuery
                    .Where(p => p.status == VerificationStatus.Verified)
                    .SumAsync(p => p.Amount);

                var pendingVerifications = await clientQuery.CountAsync(c => c.VerificationStatus == "Pending");
                var verifiedClients = await clientQuery.CountAsync(c => c.VerificationStatus == "Verified");
                var rejectedClients = await clientQuery.CountAsync(c => c.VerificationStatus == "Rejected");

                var totalDocuments = await documentQuery.CountAsync();
                var lastActivity = await paymentQuery.OrderByDescending(p => p.PaymentDate).Select(p => p.PaymentDate).FirstOrDefaultAsync();

                reports.Add(new BankPerformanceReportDTO
                {
                    BankId = bank.BankId,
                    BankCode = bank.Code,
                    BankName = bank.Name,
                    BankAddress = bank.Address,
                    TotalClients = totalClients,
                    ActiveClients = activeClients,
                    InactiveClients = inactiveClients,
                    TotalBankUsers = bank.BankUsers?.Count ?? 0,
                    TotalPayments = totalPayments,
                    PendingPayments = pendingPayments,
                    ApprovedPayments = approvedPayments,
                    RejectedPayments = rejectedPayments,
                    TotalPaymentValue = totalPaymentValue,
                    PendingVerifications = pendingVerifications,
                    VerifiedClients = verifiedClients,
                    RejectedClients = rejectedClients,
                    TotalDocuments = totalDocuments,
                    LastActivityDate = lastActivity
                });
            }

            return reports.OrderByDescending(r => r.TotalPaymentValue);
        }

        //public async Task<TransactionVolumeReportDTO> GetTransactionVolumeReportAsync(DateTime startDate, DateTime endDate)
        //{
        //    var payments = await _context.Payments
        //        .Include(p => p.Client)
        //            .ThenInclude(c => c.Bank)
        //        .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
        //        .ToListAsync();

        //    var salaryDisbursements = await _context.SalaryDisbursements
        //        .Where(s => s.CreatedAt >= startDate && s.CreatedAt <= endDate)
        //        .ToListAsync();

        //    // Payment Type Breakdown
        //    var paymentTypeBreakdown = new PaymentTypeBreakdown
        //    {
        //        RTGSCount = payments.Count(p => p.Type == PaymentType.RTGS),
        //        RTGSAmount = payments.Where(p => p.Type == PaymentType.RTGS && p.status == VerificationStatus.Verified).Sum(p => p.Amount),
        //        IMPSCount = payments.Count(p => p.Type == PaymentType.IMPS),
        //        IMPSAmount = payments.Where(p => p.Type == PaymentType.IMPS && p.status == VerificationStatus.Verified).Sum(p => p.Amount),
        //        NEFTCount = payments.Count(p => p.Type == PaymentType.NEFT),
        //        NEFTAmount = payments.Where(p => p.Type == PaymentType.NEFT && p.status == VerificationStatus.Verified).Sum(p => p.Amount)
        //    };

        //    // Payment Status Breakdown
        //    var paymentStatusBreakdown = new PaymentStatusBreakdown
        //    {
        //        PendingCount = payments.Count(p => p.status == VerificationStatus.Pending),
        //        PendingAmount = payments.Where(p => p.status == VerificationStatus.Pending).Sum(p => p.Amount),
        //        ApprovedCount = payments.Count(p => p.status == VerificationStatus.Verified),
        //        ApprovedAmount = payments.Where(p => p.status == VerificationStatus.Verified).Sum(p => p.Amount),
        //        RejectedCount = payments.Count(p => p.status == VerificationStatus.Rejected),
        //        RejectedAmount = payments.Where(p => p.status == VerificationStatus.Rejected).Sum(p => p.Amount)
        //    };

        //    // Bank-wise Transactions
        //    var bankWiseTransactions = payments
        //        .Where(p => p.status == VerificationStatus.Verified)
        //        .GroupBy(p => new { p.Client.Bank.BankId, p.Client.Bank.Name })
        //        .Select(g => new BankTransactionSummary
        //        {
        //            BankId = g.Key.BankId,
        //            BankName = g.Key.Name,
        //            TransactionCount = g.Count(),
        //            TotalAmount = g.Sum(p => p.Amount)
        //        })
        //        .OrderByDescending(b => b.TotalAmount)
        //        .ToList();

        //    // Daily Trends
        //    var dailyTrends = payments
        //        .Where(p => p.status == VerificationStatus.Verified)
        //        .GroupBy(p => p.PaymentDate.Date)
        //        .Select(g => new DailyTransactionTrend
        //        {
        //            Date = g.Key,
        //            TransactionCount = g.Count(),
        //            TotalAmount = g.Sum(p => p.Amount)
        //        })
        //        .OrderBy(d => d.Date)
        //        .ToList();

        //    return new TransactionVolumeReportDTO
        //    {
        //        StartDate = startDate,
        //        EndDate = endDate,
        //        TotalPayments = payments.Count,
        //        TotalPaymentAmount = payments.Where(p => p.status == VerificationStatus.Verified).Sum(p => p.Amount),
        //        TotalSalaryDisbursements = salaryDisbursements.Count,
        //        TotalSalaryAmount = salaryDisbursements.Sum(s => s.Amount),
        //        PaymentTypeBreakdown = paymentTypeBreakdown,
        //        PaymentStatusBreakdown = paymentStatusBreakdown,
        //        BankWiseTransactions = bankWiseTransactions,
        //        DailyTrends = dailyTrends
        //    };
        //}
        public async Task<TransactionVolumeReportDTO> GetTransactionVolumeReportAsync(DateTime startDate, DateTime endDate)
        {
            var payments = await _context.Payments
                .Include(p => p.Client)
                    .ThenInclude(c => c.Bank)
                .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                .ToListAsync();

            var salaryDisbursements = await _context.SalaryDisbursements
                .Where(s => s.CreatedAt >= startDate && s.CreatedAt <= endDate)
                .ToListAsync();

            var approvedStatus = VerificationStatus.Verified;

            var paymentTypeBreakdown = new PaymentTypeBreakdown
            {
                RTGSCount = payments.Count(p => p.Type == PaymentType.RTGS),
                RTGSAmount = payments.Where(p => p.Type == PaymentType.RTGS && p.status == approvedStatus).Sum(p => p.Amount),

                IMPSCount = payments.Count(p => p.Type == PaymentType.IMPS),
                IMPSAmount = payments.Where(p => p.Type == PaymentType.IMPS && p.status == approvedStatus).Sum(p => p.Amount),

                NEFTCount = payments.Count(p => p.Type == PaymentType.NEFT),
                NEFTAmount = payments.Where(p => p.Type == PaymentType.NEFT && p.status == approvedStatus).Sum(p => p.Amount)
            };

            var paymentStatusBreakdown = new PaymentStatusBreakdown
            {
                PendingCount = payments.Count(p => p.status == VerificationStatus.Pending),
                PendingAmount = payments.Where(p => p.status == VerificationStatus.Pending).Sum(p => p.Amount),

                ApprovedCount = payments.Count(p => p.status == approvedStatus),
                ApprovedAmount = payments.Where(p => p.status == approvedStatus).Sum(p => p.Amount),

                RejectedCount = payments.Count(p => p.status == VerificationStatus.Rejected),
                RejectedAmount = payments.Where(p => p.status == VerificationStatus.Rejected).Sum(p => p.Amount)
            };

            
           

            var bankWiseTransactions=payments.Where(p=>p.status==approvedStatus).
                GroupBy(p=>new {p.Client.Bank.BankId,p.Client.Bank.Name})
                 .Select(g => new BankTransactionSummary
                 {
                     BankId = g.Key.BankId,
                     BankName = g.Key.Name,
                     TransactionCount = g.Count(),
                     TotalAmount = g.Sum(p => p.Amount)
                 })
                .OrderByDescending(b => b.TotalAmount).
                ToList();

            




            var dailyTrends = payments
                .Where(p => p.status == approvedStatus)
                .GroupBy(p => p.PaymentDate.Date)
                .Select(g => new DailyTransactionTrend
                {
                    Date = g.Key,
                    TransactionCount = g.Count(),
                    TotalAmount = g.Sum(p => p.Amount)
                })
                .OrderBy(d => d.Date)
                .ToList();

            int approvedPayments=payments.Count(p=>p.status==approvedStatus);

            return new TransactionVolumeReportDTO
            {
                StartDate = startDate,
                EndDate = endDate,
                // can add approved only payments count if needed
                TotalPayments = payments.Count, // ALL payments
                TotalPaymentAmount = payments.Where(p => p.status == approvedStatus).Sum(p => p.Amount), // Only approved
                TotalSalaryDisbursements = salaryDisbursements.Count,
                TotalSalaryAmount = salaryDisbursements.Sum(s => s.Amount),
                PaymentTypeBreakdown = paymentTypeBreakdown,
                PaymentStatusBreakdown = paymentStatusBreakdown,
                BankWiseTransactions = bankWiseTransactions,
                DailyTrends = dailyTrends
            };
        }
        public async Task<FinancialSummaryReportDTO> GetFinancialSummaryReportAsync(DateTime startDate, DateTime endDate)
        {
            var payments = await _context.Payments
                .Include(p => p.Client)
                    .ThenInclude(c => c.Bank)
                .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate && p.status == VerificationStatus.Verified)
                .ToListAsync();

            var salaries = await _context.SalaryDisbursements
                .Include(s => s.Client)
                .Where(s => s.CreatedAt >= startDate && s.CreatedAt <= endDate)
                .ToListAsync();

            var totalPaymentsValue = payments.Sum(p => p.Amount);
            var totalSalariesValue = salaries.Sum(s => s.Amount);
            var totalClientBalance = await _context.Clients.SumAsync(c => (double)c.Balance);

            // Bank-wise Financials
            var bankWiseFinancials = await _context.Banks
                .Select(b => new BankFinancialSummary
                {
                    BankId = b.BankId,
                    BankName = b.Name,
                    ClientCount = b.Clients.Count,
                    TotalPaymentValue = b.Clients
                        .SelectMany(c => c.Payments)
                        .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate && p.status == VerificationStatus.Verified)
                        .Sum(p => p.Amount),
                    TotalSalaryValue = b.Clients
                        .SelectMany(c => c.SalaryDisbursement)
                        .Where(s => s.CreatedAt >= startDate && s.CreatedAt <= endDate)
                        .Sum(s => s.Amount),
                    AverageTransactionValue = b.Clients
                        .SelectMany(c => c.Payments)
                        .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate && p.status == VerificationStatus.Verified)
                        .Average(p => (decimal?)p.Amount) ?? 0,
                    TotalClientBalance = (decimal)b.Clients.Sum(c => c.Balance)
                })
                .OrderByDescending(b => b.TotalPaymentValue)
                .ToListAsync();

            // Top Clients by Volume
            var topClients = await _context.Clients
                .Include(c => c.Bank)
                .Include(c => c.Payments)
                .Select(c => new TopClient
                {
                    ClientId = c.ClientId,
                    ClientCode = c.Code,
                    ClientName = c.Name,
                    BankName = c.Bank.Name,
                    TransactionCount = c.Payments
                        .Count(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate && p.status == VerificationStatus.Verified),
                    TotalTransactionValue = c.Payments
                        .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate && p.status == VerificationStatus.Verified)
                        .Sum(p => p.Amount)
                })
                .Where(c => c.TransactionCount > 0)
                .OrderByDescending(c => c.TotalTransactionValue)
                .Take(10)
                .ToListAsync();

            return new FinancialSummaryReportDTO
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalMoneyFlow = totalPaymentsValue + totalSalariesValue,
                TotalPaymentsValue = totalPaymentsValue,
                TotalSalariesValue = totalSalariesValue,
                TotalClientBalance = (decimal)totalClientBalance,
                BankWiseFinancials = bankWiseFinancials,
                TopClientsByVolume = topClients,
                MonthlyTrend = new MonthlyFinancialTrend
                {
                    Month = startDate.Month,
                    Year = startDate.Year,
                    TotalPayments = totalPaymentsValue,
                    TotalSalaries = totalSalariesValue
                }
            };
        }

        // ==================== BANK USER REPORTS ====================

        public async Task<IEnumerable<TransactionReportDTO>> GetTransactionReportByClientAsync(int clientId, int bankId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Payments
                .Include(p => p.Client)
                .Include(p => p.Beneficiary)
                .Include(p => p.ApprovedBy)
                .Where(p => p.ClientId == clientId && p.Client.BankId == bankId);

            if (startDate.HasValue)
                query = query.Where(p => p.PaymentDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(p => p.PaymentDate <= endDate.Value);

            var payments = await query.OrderByDescending(p => p.PaymentDate).ToListAsync();

            return payments.Select(p => new TransactionReportDTO
            {
                PaymentId = p.PaymentId,
                PaymentType = p.Type.ToString(),
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                Status = p.status.ToString(),
                ClientName = p.Client?.Name ?? "Unknown",
                BeneficiaryName = p.Beneficiary?.Name ?? "Unknown",
                BeneficiaryAccountNumber = p.Beneficiary?.AccountNumber ?? "N/A",
                ApprovedByName = p.ApprovedBy?.Name ?? "Pending",
                Remarks = p.Remarks ?? string.Empty
            });
        }

        public async Task<CustomerOnboardingReportDTO> GetCustomerOnboardingReportAsync(int bankId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Clients
                .Include(c => c.ApprovedByBankUser)
                .Include(c => c.Documents)
                .Where(c => c.BankId == bankId);

            if (startDate.HasValue)
                query = query.Where(c => c.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(c => c.CreatedAt <= endDate.Value);

            var clients = await query.ToListAsync();

            var clientDetails = clients.Select(c => new ClientOnboardingDetail
            {
                ClientId = c.ClientId,
                ClientCode = c.Code,
                ClientName = c.Name,
                Email = c.Email,
                BusinessType = c.BusinessType,
                VerificationStatus = c.VerificationStatus,
                CreatedAt = c.CreatedAt,
                ApprovedByName = c.ApprovedByBankUser?.Name ?? "Pending",
                DocumentCount = c.Documents?.Count ?? 0,
                DaysToVerify = c.VerificationStatus == "Verified" ? (DateTime.UtcNow - c.CreatedAt).Days : 0
            }).ToList();

            return new CustomerOnboardingReportDTO
            {
                StartDate = startDate ?? DateTime.MinValue,
                EndDate = endDate ?? DateTime.UtcNow,
                TotalOnboarded = clients.Count,
                PendingVerifications = clients.Count(c => c.VerificationStatus == "Pending"),
                ApprovedClients = clients.Count(c => c.VerificationStatus == "Verified"),
                RejectedClients = clients.Count(c => c.VerificationStatus == "Rejected"),
                AverageOnboardingDays = clients.Where(c => c.VerificationStatus == "Verified")
                    .Average(c => (double?)(DateTime.UtcNow - c.CreatedAt).Days) ?? 0,
                DocumentsUploaded = clients.Sum(c => c.Documents?.Count ?? 0),
                ClientDetails = clientDetails
            };
        }

        public async Task<PaymentApprovalReportDTO> GetPaymentApprovalReportAsync(int bankId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Payments
                .Include(p => p.Client)
                
                .Include(p => p.Beneficiary)
                .Include(p => p.ApprovedBy)
                .Where(p => p.Client.BankId == bankId);

            if (startDate.HasValue)
                query = query.Where(p => p.PaymentDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(p => p.PaymentDate <= endDate.Value);

            var payments = await query.ToListAsync();

            var bankUsers = await _context.BankUsers.ToListAsync();

            // Bank User Performance
            var bankUserPerformance = payments
                .Where(p => p.BankUserId.HasValue)
                .Join(
                    bankUsers,                              // second table
                    p => p.BankUserId,                      // key from payments
                    b => b.BankUserId,                      // key from bankUsers
                    (p, b) => new { Payment = p, BankUser = b } // result selector
                )
                .GroupBy(x => new
                {
                    x.BankUser.BankUserId,
                    x.BankUser.Name,
                    x.BankUser.Code
                })
                .Select(g => new BankUserApprovalPerformance
                {
                    BankUserName = g.Key.Name ?? "Unknown",
                    BankUserCode = g.Key.Code ?? "N/A",   
                    TotalApproved = g.Count(x => x.Payment.status == VerificationStatus.Verified),
                    TotalRejected = g.Count(x => x.Payment.status == VerificationStatus.Rejected),
                    TotalAmountApproved = g
                        .Where(x => x.Payment.status == VerificationStatus.Verified)
                        .Sum(x => x.Payment.Amount)
                })
                .ToList();


            // High Value Transactions (above 100,000)
            var highValueTransactions = payments
                .Where(p => p.Amount > 100000)
                .OrderByDescending(p => p.Amount)
                .Take(20)
                .Select(p => new HighValueTransaction
                {
                    PaymentId = p.PaymentId,
                    Amount = p.Amount,
                    ClientName = p.Client.Name,
                    BeneficiaryName = p.Beneficiary.Name,
                    PaymentDate = p.PaymentDate,
                    Status = p.status.ToString()
                })
                .ToList();



           


            return new PaymentApprovalReportDTO
            {
                StartDate = startDate ?? DateTime.MinValue,
                EndDate = endDate ?? DateTime.UtcNow,
                TotalPayments = payments.Count,
                PendingApprovals = payments.Count(p => p.status == VerificationStatus.Pending),
                ApprovedPayments = payments.Count(p => p.status == VerificationStatus.Verified),
                RejectedPayments = payments.Count(p => p.status == VerificationStatus.Rejected),
                TotalApprovedAmount = payments.Where(p => p.status == VerificationStatus.Verified).Sum(p => p.Amount),
                TotalRejectedAmount = payments.Where(p => p.status == VerificationStatus.Rejected).Sum(p => p.Amount),
                BankUserPerformance = bankUserPerformance,
                HighValueTransactions = highValueTransactions
            };
        }

        public async Task<IEnumerable<ClientActivityReportDTO>> GetClientActivityReportAsync(int bankId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Clients
                .Include(c => c.Payments)
                .Include(c => c.Employees)
                .Include(c => c.Beneficiaries)
                .Include(c => c.SalaryDisbursement)
                .Where(c => c.BankId == bankId);

            var clients = await query.ToListAsync();

            return clients.Select(c =>
            {
                var paymentsQuery = c.Payments.AsEnumerable();
                var salariesQuery = c.SalaryDisbursement.AsEnumerable();

                if (startDate.HasValue)
                {
                    paymentsQuery = paymentsQuery.Where(p => p.PaymentDate >= startDate.Value);
                    salariesQuery = salariesQuery.Where(s => s.CreatedAt >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    paymentsQuery = paymentsQuery.Where(p => p.PaymentDate <= endDate.Value);
                    salariesQuery = salariesQuery.Where(s => s.CreatedAt <= endDate.Value);
                }

                var lastPayment = paymentsQuery.OrderByDescending(p => p.PaymentDate).FirstOrDefault();
                var lastSalary = salariesQuery.OrderByDescending(s => s.CreatedAt).FirstOrDefault();
                var lastActivity = new[] { lastPayment?.PaymentDate, lastSalary?.CreatedAt }
                    .Where(d => d.HasValue)
                    .Max() ?? c.CreatedAt;

                return new ClientActivityReportDTO
                {
                    ClientId = c.ClientId,
                    ClientCode = c.Code,
                    ClientName = c.Name,
                    Email = c.Email,
                    IsActive = c.IsActive,
                    TotalPayments = paymentsQuery.Count(),
                    TotalPaymentValue = paymentsQuery.Where(p => p.status == VerificationStatus.Verified).Sum(p => p.Amount),
                    TotalEmployees = c.Employees?.Count ?? 0,
                    TotalBeneficiaries = c.Beneficiaries?.Count ?? 0,
                    TotalSalaryDisbursements = salariesQuery.Count(),
                    TotalSalaryValue = salariesQuery.Sum(s => s.Amount),
                    LastActivityDate = lastActivity,
                    CurrentBalance = (decimal)c.Balance
                };
            }).OrderByDescending(c => c.TotalPaymentValue);
        }

        public async Task<IEnumerable<TransactionReportDTO>> GetAllTransactionsByBankAsync(int bankId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Payments
                .Include(p => p.Client)
                .Include(p => p.Beneficiary)
                .Include(p => p.ApprovedBy)
                .Where(p => p.Client.BankId == bankId);

            if (startDate.HasValue)
                query = query.Where(p => p.PaymentDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(p => p.PaymentDate <= endDate.Value);

            var payments = await query.OrderByDescending(p => p.PaymentDate).ToListAsync();

            return payments.Select(p => new TransactionReportDTO
            {
                PaymentId = p.PaymentId,
                PaymentType = p.Type.ToString(),
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                Status = p.status.ToString(),
                ClientName = p.Client?.Name ?? "Unknown",
                BeneficiaryName = p.Beneficiary?.Name ?? "Unknown",
                BeneficiaryAccountNumber = p.Beneficiary?.AccountNumber ?? "N/A",
                ApprovedByName = p.ApprovedBy?.Name ?? "Pending",
                Remarks = p.Remarks ?? string.Empty
            });
        }
    }
}