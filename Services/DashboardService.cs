// Services/DashboardService.cs
using Banking_Payments.Context;
using Banking_Payments.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Banking_Payments.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsDTO> GetDashboardStatsAsync()
        {
            var totalBanks = await _context.Banks.CountAsync();
            var activeBanks = await _context.Banks.CountAsync(b => b.IsActive);
            var activeBankUsers = await _context.BankUsers.CountAsync();

            // Reports - placeholder for future implementation
            var reportsGenerated = 0;

            return new DashboardStatsDTO
            {
                TotalBanks = totalBanks,
                ActiveBanks = activeBanks,
                ActiveBankUsers = activeBankUsers,
                ReportsGenerated = reportsGenerated
            };
        }

        public async Task<List<BankDistributionDTO>> GetBankDistributionAsync()
        {
            var banksWithClients = await _context.Banks
                .Where(b => b.IsActive)
                .Select(b => new BankDistributionDTO
                {
                    BankId = b.BankId,
                    BankName = b.Name,
                    ClientCount = b.Clients != null ? b.Clients.Count : 0,
                    Percentage = 0 // Will calculate after
                })
                .ToListAsync();

            var totalClients = banksWithClients.Sum(b => b.ClientCount);

            if (totalClients > 0)
            {
                foreach (var bank in banksWithClients)
                {
                    bank.Percentage = (decimal)bank.ClientCount / totalClients * 100;
                }
            }

            return banksWithClients.OrderByDescending(b => b.ClientCount).ToList();
        }

        public async Task<List<RecentActivityDTO>> GetRecentActivitiesAsync(int limit = 10)
        {
            var activities = new List<RecentActivityDTO>();

            // Get recent bank creations
            var recentBanks = await _context.Banks
                .OrderByDescending(b => b.CreatedAt)
                .Take(limit)
                .Select(b => new RecentActivityDTO
                {
                    Id = b.BankId,
                    Description = $"Bank {b.Name} created",
                    Timestamp = b.CreatedAt,
                    Type = "bank"
                })
                .ToListAsync();

            activities.AddRange(recentBanks);

            // Sort by timestamp and take only the limit
            return activities
                .OrderByDescending(a => a.Timestamp)
                .Take(limit)
                .ToList();
        }
    }
}