using Banking_Payments.Models.DTO.Reports;
using Banking_Payments.Repositories;

namespace Banking_Payments.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly ILogger<ReportService> _logger;

        public ReportService(IReportRepository reportRepository, ILogger<ReportService> logger)
        {
            _reportRepository = reportRepository;
            _logger = logger;
        }

        // ==================== SUPER ADMIN REPORTS ====================

        public async Task<SystemOverviewReportDTO> GetSystemOverviewReportAsync()
        {
            try
            {
                _logger.LogInformation("Generating system overview report");
                var report = await _reportRepository.GetSystemOverviewAsync();
                _logger.LogInformation("System overview report generated successfully");
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate system overview report");
                throw;
            }
        }

        public async Task<IEnumerable<BankPerformanceReportDTO>> GetBankPerformanceReportAsync(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                _logger.LogInformation("Generating bank performance report from {StartDate} to {EndDate}", startDate, endDate);

                // Set default date range if not provided
                startDate ??= DateTime.UtcNow.AddMonths(-1);
                endDate ??= DateTime.UtcNow;

                var report = await _reportRepository.GetBankPerformanceReportAsync(startDate, endDate);
                _logger.LogInformation("Bank performance report generated successfully with {Count} banks", report.Count());
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate bank performance report");
                throw;
            }
        }

        public async Task<TransactionVolumeReportDTO> GetTransactionVolumeReportAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Validate date range
                if (endDate < startDate)
                    throw new ArgumentException("End date must be after start date");

                _logger.LogInformation("Generating transaction volume report from {StartDate} to {EndDate}", startDate, endDate);
                var report = await _reportRepository.GetTransactionVolumeReportAsync(startDate, endDate);
                _logger.LogInformation("Transaction volume report generated successfully");
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate transaction volume report");
                throw;
            }
        }

        public async Task<FinancialSummaryReportDTO> GetFinancialSummaryReportAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Validate date range
                if (endDate < startDate)
                    throw new ArgumentException("End date must be after start date");

                _logger.LogInformation("Generating financial summary report from {StartDate} to {EndDate}", startDate, endDate);
                var report = await _reportRepository.GetFinancialSummaryReportAsync(startDate, endDate);
                _logger.LogInformation("Financial summary report generated successfully");
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate financial summary report");
                throw;
            }
        }

        // ==================== BANK USER REPORTS ====================

        public async Task<IEnumerable<TransactionReportDTO>> GetTransactionReportByClientAsync(int clientId, int bankId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                if (clientId <= 0)
                    throw new ArgumentException("Invalid client ID");

                if (bankId <= 0)
                    throw new ArgumentException("Invalid bank ID");

                _logger.LogInformation("Generating transaction report for Client {ClientId} in Bank {BankId}", clientId, bankId);

                var report = await _reportRepository.GetTransactionReportByClientAsync(clientId, bankId, startDate, endDate);
                _logger.LogInformation("Transaction report generated with {Count} transactions", report.Count());
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate transaction report for client {ClientId}", clientId);
                throw;
            }
        }

        public async Task<CustomerOnboardingReportDTO> GetCustomerOnboardingReportAsync(int bankId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                if (bankId <= 0)
                    throw new ArgumentException("Invalid bank ID");

                _logger.LogInformation("Generating customer onboarding report for Bank {BankId}", bankId);

                var report = await _reportRepository.GetCustomerOnboardingReportAsync(bankId, startDate, endDate);
                _logger.LogInformation("Customer onboarding report generated with {Count} customers", report.TotalOnboarded);
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate customer onboarding report for bank {BankId}", bankId);
                throw;
            }
        }

        public async Task<PaymentApprovalReportDTO> GetPaymentApprovalReportAsync(int bankId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                if (bankId <= 0)
                    throw new ArgumentException("Invalid bank ID");

                _logger.LogInformation("Generating payment approval report for Bank {BankId}", bankId);

                var report = await _reportRepository.GetPaymentApprovalReportAsync(bankId, startDate, endDate);
                _logger.LogInformation("Payment approval report generated with {Count} payments", report.TotalPayments);
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate payment approval report for bank {BankId}", bankId);
                throw;
            }
        }

        public async Task<IEnumerable<ClientActivityReportDTO>> GetClientActivityReportAsync(int bankId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                if (bankId <= 0)
                    throw new ArgumentException("Invalid bank ID");

                _logger.LogInformation("Generating client activity report for Bank {BankId}", bankId);

                var report = await _reportRepository.GetClientActivityReportAsync(bankId, startDate, endDate);
                _logger.LogInformation("Client activity report generated with {Count} clients", report.Count());
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate client activity report for bank {BankId}", bankId);
                throw;
            }
        }

        public async Task<IEnumerable<TransactionReportDTO>> GetAllTransactionsByBankAsync(int bankId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                if (bankId <= 0)
                    throw new ArgumentException("Invalid bank ID");

                _logger.LogInformation("Generating all transactions report for Bank {BankId}", bankId);

                var report = await _reportRepository.GetAllTransactionsByBankAsync(bankId, startDate, endDate);
                _logger.LogInformation("All transactions report generated with {Count} transactions", report.Count());
                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate all transactions report for bank {BankId}", bankId);
                throw;
            }
        }
    }
}