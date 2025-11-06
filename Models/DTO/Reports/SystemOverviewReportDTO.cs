namespace Banking_Payments.Models.DTO.Reports
{
    public class SystemOverviewReportDTO
    {
        public int TotalBanks { get; set; }
        public int ActiveBanks { get; set; }
        public int InactiveBanks { get; set; }
        public int TotalClients { get; set; }
        public int TotalBankUsers { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalBeneficiaries { get; set; }
        public int TotalPayments { get; set; }
        public int PendingPayments { get; set; }
        public int ApprovedPayments { get; set; }
        public int RejectedPayments { get; set; }
        public decimal TotalPaymentValue { get; set; }
        public int TotalSalaryDisbursements { get; set; }
        public decimal TotalSalaryValue { get; set; }
        public int PendingVerifications { get; set; }
        public int VerifiedClients { get; set; }
        public int RejectedClients { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    }
}