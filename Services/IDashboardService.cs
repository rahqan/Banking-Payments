// Services/IDashboardService.cs
using Banking_Payments.Models.DTO;

namespace Banking_Payments.Services
{
    public interface IDashboardService
    {
        Task<DashboardStatsDTO> GetDashboardStatsAsync();
        Task<List<BankDistributionDTO>> GetBankDistributionAsync();
        Task<List<RecentActivityDTO>> GetRecentActivitiesAsync(int limit = 10);
    }
}