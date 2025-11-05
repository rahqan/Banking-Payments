using Banking_Payments.Models.Enums;
using Banking_Payments.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banking_Payments.Controllers
{
    [Route("api/Dashboard")]
    [ApiController]
    //[Authorize(Roles = nameof(Role.SuperAdmin))]
    //[Authorize(Roles = "SuperAdmin")]
    //[Authorize(Roles = "SuperAdmin,Role.SuperAdmin")]

    [Authorize(Roles = "SuperAdmin")]

    public class SuperAdminDashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public SuperAdminDashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats() =>
            Ok(await _dashboardService.GetDashboardStatsAsync());

        [HttpGet("bank-distribution")]
        public async Task<IActionResult> GetBankDistribution() =>
            Ok(await _dashboardService.GetBankDistributionAsync());

        [HttpGet("recent-activities")]
        public async Task<IActionResult> GetRecentActivities([FromQuery] int limit = 10) =>
            Ok(await _dashboardService.GetRecentActivitiesAsync(limit));
    }

}
