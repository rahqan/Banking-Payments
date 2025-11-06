using Banking_Payments.Models.DTO.Reports;
using Banking_Payments.Models.Enums;
using Banking_Payments.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banking_Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(Role.SuperAdmin))]
    public class SuperAdminReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IPdfExportService _pdfExportService;
        private readonly ILogger<SuperAdminReportController> _logger;

        public SuperAdminReportController(
            IReportService reportService,
            IPdfExportService pdfExportService,
            ILogger<SuperAdminReportController> logger)
        {
            _reportService = reportService;
            _pdfExportService = pdfExportService;
            _logger = logger;
        }

        // Helper method to get AdminId from claims
        private int GetAdminIdFromClaims()
        {
            var adminIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(adminIdClaim) || !int.TryParse(adminIdClaim, out int adminId))
            {
                throw new UnauthorizedAccessException("AdminId claim missing or invalid");
            }
            return adminId;
        }

        /// <summary>
        /// Get system overview report with key metrics
        /// </summary>
        /// <returns>System overview statistics</returns>
        [HttpGet("system-overview")]
        public async Task<ActionResult<SystemOverviewReportDTO>> GetSystemOverview()
        {
            try
            {
                var adminId = GetAdminIdFromClaims();
                _logger.LogInformation("Admin {AdminId} requested system overview report", adminId);

                var report = await _reportService.GetSystemOverviewReportAsync();
                return Ok(report);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate system overview report");
                return StatusCode(500, new { message = "An error occurred while generating the report." });
            }
        }

        /// <summary>
        /// Get bank performance report with optional date filtering
        /// </summary>
        /// <param name="startDate">Optional start date</param>
        /// <param name="endDate">Optional end date</param>
        /// <returns>Bank performance statistics</returns>
        [HttpGet("bank-performance")]
        public async Task<ActionResult<IEnumerable<BankPerformanceReportDTO>>> GetBankPerformance(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var adminId = GetAdminIdFromClaims();
                _logger.LogInformation("Admin {AdminId} requested bank performance report", adminId);

                var report = await _reportService.GetBankPerformanceReportAsync(startDate, endDate);
                return Ok(report);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate bank performance report");
                return StatusCode(500, new { message = "An error occurred while generating the report." });
            }
        }

        /// <summary>
        /// Get transaction volume report with date range
        /// </summary>
        /// <param name="startDate">Start date (required)</param>
        /// <param name="endDate">End date (required)</param>
        /// <returns>Transaction volume statistics</returns>
        [HttpGet("transaction-volume")]
        public async Task<ActionResult<TransactionVolumeReportDTO>> GetTransactionVolume(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var adminId = GetAdminIdFromClaims();

                // Validate dates
                if (startDate == default || endDate == default)
                {
                    return BadRequest(new { message = "Both start date and end date are required" });
                }

                if (endDate < startDate)
                {
                    return BadRequest(new { message = "End date must be after start date" });
                }

                _logger.LogInformation("Admin {AdminId} requested transaction volume report from {StartDate} to {EndDate}",
                    adminId, startDate, endDate);

                var report = await _reportService.GetTransactionVolumeReportAsync(startDate, endDate);
                return Ok(report);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate transaction volume report");
                return StatusCode(500, new { message = "An error occurred while generating the report." });
            }
        }

        /// <summary>
        /// Get financial summary report with date range
        /// </summary>
        /// <param name="startDate">Start date (required)</param>
        /// <param name="endDate">End date (required)</param>
        /// <returns>Financial summary statistics</returns>
        [HttpGet("financial-summary")]
        public async Task<ActionResult<FinancialSummaryReportDTO>> GetFinancialSummary(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var adminId = GetAdminIdFromClaims();

                // Validate dates
                if (startDate == default || endDate == default)
                {
                    return BadRequest(new { message = "Both start date and end date are required" });
                }

                if (endDate < startDate)
                {
                    return BadRequest(new { message = "End date must be after start date" });
                }

                _logger.LogInformation("Admin {AdminId} requested financial summary report from {StartDate} to {EndDate}",
                    adminId, startDate, endDate);

                var report = await _reportService.GetFinancialSummaryReportAsync(startDate, endDate);
                return Ok(report);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate financial summary report");
                return StatusCode(500, new { message = "An error occurred while generating the report." });
            }
        }

        /// <summary>
        /// Get quick stats for dashboard
        /// </summary>
        /// <returns>Quick statistics for admin dashboard</returns>
        [HttpGet("quick-stats")]
        public async Task<ActionResult> GetQuickStats()
        {
            try
            {
                var adminId = GetAdminIdFromClaims();
                _logger.LogInformation("Admin {AdminId} requested quick stats", adminId);

                var overview = await _reportService.GetSystemOverviewReportAsync();

                var quickStats = new
                {
                    totalBanks = overview.TotalBanks,
                    totalClients = overview.TotalClients,
                    pendingPayments = overview.PendingPayments,
                    pendingVerifications = overview.PendingVerifications,
                    totalTransactionValue = overview.TotalPaymentValue,
                    activeBanks = overview.ActiveBanks,
                    todayDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
                };

                return Ok(quickStats);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate quick stats");
                return StatusCode(500, new { message = "An error occurred while generating quick stats." });
            }
        }

        // ==================== PDF EXPORT ENDPOINTS ====================

        /// <summary>
        /// Download system overview report as PDF
        /// </summary>
        [HttpGet("system-overview/pdf")]
        public async Task<IActionResult> DownloadSystemOverviewPdf()
        {
            try
            {
                var adminId = GetAdminIdFromClaims();
                _logger.LogInformation("Admin {AdminId} requested system overview PDF", adminId);

                var report = await _reportService.GetSystemOverviewReportAsync();
                var pdfBytes = _pdfExportService.GenerateSystemOverviewPdf(report);

                return File(pdfBytes, "application/pdf", $"SystemOverview_{DateTime.UtcNow:yyyyMMdd_HHmmss}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate system overview PDF");
                return StatusCode(500, new { message = "Failed to generate PDF report" });
            }
        }

        /// <summary>
        /// Download bank performance report as PDF
        /// </summary>
        [HttpGet("bank-performance/pdf")]
        public async Task<IActionResult> DownloadBankPerformancePdf(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var adminId = GetAdminIdFromClaims();
                _logger.LogInformation("Admin {AdminId} requested bank performance PDF", adminId);

                var report = await _reportService.GetBankPerformanceReportAsync(startDate, endDate);
                var pdfBytes = _pdfExportService.GenerateBankPerformancePdf(report, startDate, endDate);

                return File(pdfBytes, "application/pdf", $"BankPerformance_{DateTime.UtcNow:yyyyMMdd_HHmmss}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate bank performance PDF");
                return StatusCode(500, new { message = "Failed to generate PDF report" });
            }
        }

        /// <summary>
        /// Download transaction volume report as PDF
        /// </summary>
        [HttpGet("transaction-volume/pdf")]
        public async Task<IActionResult> DownloadTransactionVolumePdf(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var adminId = GetAdminIdFromClaims();

                if (startDate == default || endDate == default)
                {
                    return BadRequest(new { message = "Both start date and end date are required" });
                }

                _logger.LogInformation("Admin {AdminId} requested transaction volume PDF", adminId);

                var report = await _reportService.GetTransactionVolumeReportAsync(startDate, endDate);
                var pdfBytes = _pdfExportService.GenerateTransactionVolumePdf(report);

                return File(pdfBytes, "application/pdf", $"TransactionVolume_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate transaction volume PDF");
                return StatusCode(500, new { message = "Failed to generate PDF report" });
            }
        }

        /// <summary>
        /// Download financial summary report as PDF
        /// </summary>
        [HttpGet("financial-summary/pdf")]
        public async Task<IActionResult> DownloadFinancialSummaryPdf(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var adminId = GetAdminIdFromClaims();

                if (startDate == default || endDate == default)
                {
                    return BadRequest(new { message = "Both start date and end date are required" });
                }

                _logger.LogInformation("Admin {AdminId} requested financial summary PDF", adminId);

                var report = await _reportService.GetFinancialSummaryReportAsync(startDate, endDate);
                var pdfBytes = _pdfExportService.GenerateFinancialSummaryPdf(report);

                return File(pdfBytes, "application/pdf", $"FinancialSummary_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate financial summary PDF");
                return StatusCode(500, new { message = "Failed to generate PDF report" });
            }
        }
    }
}