namespace Banking_Payments.Models.DTO
{
   
    public class DashboardStatsDTO
    {
        public int TotalBanks { get; set; }
        public int ActiveBanks { get; set; }
        public int ActiveBankUsers { get; set; }
        public int ReportsGenerated { get; set; }
    }

    public class BankDistributionDTO
    {
        public string BankName { get; set; }
        public int BankId { get; set; }
        public int ClientCount { get; set; }
        public decimal Percentage { get; set; }
    }

    public class RecentActivityDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; } // 'bank', 'user', 'report'
    }
}

