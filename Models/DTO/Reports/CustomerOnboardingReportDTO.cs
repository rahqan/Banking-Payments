namespace Banking_Payments.Models.DTO.Reports
{
    public class CustomerOnboardingReportDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalOnboarded { get; set; }
        public int PendingVerifications { get; set; }
        public int ApprovedClients { get; set; }
        public int RejectedClients { get; set; }
        public double AverageOnboardingDays { get; set; }
        public int DocumentsUploaded { get; set; }
        public List<ClientOnboardingDetail> ClientDetails { get; set; }
    }

    public class ClientOnboardingDetail
    {
        public int ClientId { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string BusinessType { get; set; }
        public string VerificationStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ApprovedByName { get; set; }
        public int DocumentCount { get; set; }
        public int DaysToVerify { get; set; }
    }
}
