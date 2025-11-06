using Banking_Payments.Models.DTO.Reports;
using Banking_Payments.Models.Enums;
using Banking_Payments.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banking_Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(Role.BankUser))]
    public class BankUserReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IPdfExportService _pdfExportService;
        private readonly ILogger<BankUserReportController> _logger;

        public BankUserReportController(
            IReportService reportService,
            IPdfExportService pdfExportService,
            ILogger<BankUserReportController> logger)
        {
            _reportService = reportService;
            _pdfExportService = pdfExportService;
            _logger = logger;
        }

        // Helper method to get BankId from claims
        private int GetBankIdFromClaims()
        {
            var bankIdClaim = User.FindFirst("BankId")?.Value;
            if (string.IsNullOrEmpty(bankIdClaim) || !int.TryParse(bankIdClaim, out int bankId))
            {
                throw new UnauthorizedAccessException("BankId claim missing or invalid");
            }
            return bankId;
        }

        // Helper method to get BankUserId from claims
        private int GetBankUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int bankUserId))
            {
                throw new UnauthorizedAccessException("BankUserId claim missing or invalid");
            }
            return bankUserId;
        }

        /// <summary>
        /// Get transaction report for a specific client (FR5)
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="startDate">Optional start date</param>
        /// <param name="endDate">Optional end date</param>
        /// <returns>Transaction report for the client</returns>
        [HttpGet("transactions/client/{clientId}")]
        public async Task<ActionResult<IEnumerable<TransactionReportDTO>>> GetTransactionReportByClient(
            int clientId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            if (clientId <= 0)
            {
                return BadRequest(new { message = "Invalid client ID" });
            }

            try
            {
                var bankId = GetBankIdFromClaims();
                var bankUserId = GetBankUserIdFromClaims();

                _logger.LogInformation("BankUser {BankUserId} requested transaction report for Client {ClientId}",
                    bankUserId, clientId);

                var report = await _reportService.GetTransactionReportByClientAsync(clientId, bankId, startDate, endDate);
                return Ok(report);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate transaction report for client {ClientId}", clientId);
                return StatusCode(500, new { message = "An error occurred while generating the report." });
            }
        }

        /// <summary>
        /// Get all transactions for the bank
        /// </summary>
        /// <param name="startDate">Optional start date</param>
        /// <param name="endDate">Optional end date</param>
        /// <returns>All transactions for the bank</returns>
        [HttpGet("transactions/all")]
        public async Task<ActionResult<IEnumerable<TransactionReportDTO>>> GetAllTransactions(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var bankId = GetBankIdFromClaims();
                var bankUserId = GetBankUserIdFromClaims();

                _logger.LogInformation("BankUser {BankUserId} requested all transactions report", bankUserId);

                var report = await _reportService.GetAllTransactionsByBankAsync(bankId, startDate, endDate);
                return Ok(report);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate all transactions report");
                return StatusCode(500, new { message = "An error occurred while generating the report." });
            }
        }

        /// <summary>
        /// Get customer onboarding report
        /// </summary>
        /// <param name="startDate">Optional start date</param>
        /// <param name="endDate">Optional end date</param>
        /// <returns>Customer onboarding statistics</returns>
        [HttpGet("customer-onboarding")]
        public async Task<ActionResult<CustomerOnboardingReportDTO>> GetCustomerOnboardingReport(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var bankId = GetBankIdFromClaims();
                var bankUserId = GetBankUserIdFromClaims();

                _logger.LogInformation("BankUser {BankUserId} requested customer onboarding report", bankUserId);

                var report = await _reportService.GetCustomerOnboardingReportAsync(bankId, startDate, endDate);
                return Ok(report);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate customer onboarding report");
                return StatusCode(500, new { message = "An error occurred while generating the report." });
            }
        }

        /// <summary>
        /// Get payment approval report
        /// </summary>
        /// <param name="startDate">Optional start date</param>
        /// <param name="endDate">Optional end date</param>
        /// <returns>Payment approval statistics</returns>
        [HttpGet("payment-approval")]
        public async Task<ActionResult<PaymentApprovalReportDTO>> GetPaymentApprovalReport(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var bankId = GetBankIdFromClaims();
                var bankUserId = GetBankUserIdFromClaims();

                _logger.LogInformation("BankUser {BankUserId} requested payment approval report", bankUserId);

                var report = await _reportService.GetPaymentApprovalReportAsync(bankId, startDate, endDate);
                return Ok(report);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate payment approval report");
                return StatusCode(500, new { message = "An error occurred while generating the report." });
            }
        }

        /// <summary>
        /// Get client activity report
        /// </summary>
        /// <param name="startDate">Optional start date</param>
        /// <param name="endDate">Optional end date</param>
        /// <returns>Client activity statistics</returns>
        [HttpGet("client-activity")]
        public async Task<ActionResult<IEnumerable<ClientActivityReportDTO>>> GetClientActivityReport(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var bankId = GetBankIdFromClaims();
                var bankUserId = GetBankUserIdFromClaims();

                _logger.LogInformation("BankUser {BankUserId} requested client activity report", bankUserId);

                var report = await _reportService.GetClientActivityReportAsync(bankId, startDate, endDate);
                return Ok(report);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate client activity report");
                return StatusCode(500, new { message = "An error occurred while generating the report." });
            }
        }

        /// <summary>
        /// Get dashboard statistics for bank user
        /// </summary>
        /// <returns>Quick statistics for bank user dashboard</returns>
        [HttpGet("dashboard-stats")]
        public async Task<ActionResult> GetDashboardStats()
        {
            try
            {
                var bankId = GetBankIdFromClaims();
                var bankUserId = GetBankUserIdFromClaims();

                _logger.LogInformation("BankUser {BankUserId} requested dashboard stats", bankUserId);

                // Get current month data
                var startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
                var endDate = DateTime.UtcNow;

                var onboardingReport = await _reportService.GetCustomerOnboardingReportAsync(bankId, startDate, endDate);
                var paymentReport = await _reportService.GetPaymentApprovalReportAsync(bankId, startDate, endDate);
                var clientActivity = await _reportService.GetClientActivityReportAsync(bankId, startDate, endDate);

                var dashboardStats = new
                {
                    pendingVerifications = onboardingReport.PendingVerifications,
                    pendingPayments = paymentReport.PendingApprovals,
                    totalClients = clientActivity.Count(),
                    activeClients = clientActivity.Count(c => c.IsActive),
                    thisMonthOnboarding = onboardingReport.TotalOnboarded,
                    thisMonthPayments = paymentReport.TotalPayments,
                    thisMonthPaymentValue = paymentReport.TotalApprovedAmount,
                    highValuePendingCount = paymentReport.HighValueTransactions.Count(t => t.Status == "Pending")
                };

                return Ok(dashboardStats);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate dashboard stats");
                return StatusCode(500, new { message = "An error occurred while generating dashboard stats." });
            }
        }

        // ==================== PDF EXPORT ENDPOINTS ====================

        /// <summary>
        /// Download transaction report for a client as PDF
        /// </summary>
        [HttpGet("transactions/client/{clientId}/pdf")]
        public async Task<IActionResult> DownloadClientTransactionsPdf(
            int clientId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            if (clientId <= 0)
            {
                return BadRequest(new { message = "Invalid client ID" });
            }

            try
            {
                var bankId = GetBankIdFromClaims();
                var bankUserId = GetBankUserIdFromClaims();

                _logger.LogInformation("BankUser {BankUserId} requested transaction PDF for Client {ClientId}",
                    bankUserId, clientId);

                var report = await _reportService.GetTransactionReportByClientAsync(clientId, bankId, startDate, endDate);
                var pdfBytes = _pdfExportService.GenerateTransactionReportPdf(report, $"Client {clientId}", startDate, endDate);

                return File(pdfBytes, "application/pdf", $"Transactions_Client{clientId}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate transaction PDF for client {ClientId}", clientId);
                return StatusCode(500, new { message = "Failed to generate PDF report" });
            }
        }

        /// <summary>
        /// Download all transactions as PDF
        /// </summary>
        [HttpGet("transactions/all/pdf")]
        public async Task<IActionResult> DownloadAllTransactionsPdf(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var bankId = GetBankIdFromClaims();
                var bankUserId = GetBankUserIdFromClaims();

                _logger.LogInformation("BankUser {BankUserId} requested all transactions PDF", bankUserId);

                var report = await _reportService.GetAllTransactionsByBankAsync(bankId, startDate, endDate);
                var pdfBytes = _pdfExportService.GenerateTransactionReportPdf(report, "All Clients", startDate, endDate);

                return File(pdfBytes, "application/pdf", $"AllTransactions_{DateTime.UtcNow:yyyyMMdd_HHmmss}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate all transactions PDF");
                return StatusCode(500, new { message = "Failed to generate PDF report" });
            }
        }

        /// <summary>
        /// Download customer onboarding report as PDF
        /// </summary>
        [HttpGet("customer-onboarding/pdf")]
        public async Task<IActionResult> DownloadCustomerOnboardingPdf(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var bankId = GetBankIdFromClaims();
                var bankUserId = GetBankUserIdFromClaims();

                _logger.LogInformation("BankUser {BankUserId} requested customer onboarding PDF", bankUserId);

                var report = await _reportService.GetCustomerOnboardingReportAsync(bankId, startDate, endDate);
                var pdfBytes = _pdfExportService.GenerateCustomerOnboardingPdf(report);

                return File(pdfBytes, "application/pdf", $"CustomerOnboarding_{DateTime.UtcNow:yyyyMMdd_HHmmss}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate customer onboarding PDF");
                return StatusCode(500, new { message = "Failed to generate PDF report" });
            }
        }

        /// <summary>
        /// Download payment approval report as PDF
        /// </summary>
        [HttpGet("payment-approval/pdf")]
        public async Task<IActionResult> DownloadPaymentApprovalPdf(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var bankId = GetBankIdFromClaims();
                var bankUserId = GetBankUserIdFromClaims();

                _logger.LogInformation("BankUser {BankUserId} requested payment approval PDF", bankUserId);

                var report = await _reportService.GetPaymentApprovalReportAsync(bankId, startDate, endDate);
                var pdfBytes = _pdfExportService.GeneratePaymentApprovalPdf(report);

                return File(pdfBytes, "application/pdf", $"PaymentApproval_{DateTime.UtcNow:yyyyMMdd_HHmmss}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate payment approval PDF");
                return StatusCode(500, new { message = "Failed to generate PDF report" });
            }
        }

        /// <summary>
        /// Download client activity report as PDF
        /// </summary>
        [HttpGet("client-activity/pdf")]
        public async Task<IActionResult> DownloadClientActivityPdf(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var bankId = GetBankIdFromClaims();
                var bankUserId = GetBankUserIdFromClaims();

                _logger.LogInformation("BankUser {BankUserId} requested client activity PDF", bankUserId);

                var report = await _reportService.GetClientActivityReportAsync(bankId, startDate, endDate);
                var pdfBytes = _pdfExportService.GenerateClientActivityPdf(report);

                return File(pdfBytes, "application/pdf", $"ClientActivity_{DateTime.UtcNow:yyyyMMdd_HHmmss}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate client activity PDF");
                return StatusCode(500, new { message = "Failed to generate PDF report" });
            }
        }
    }
}