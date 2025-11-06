namespace Banking_Payments.Models.DTO.Reports
{
    public class BankPerformanceReportDTO
    {
        public int BankId { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public int TotalClients { get; set; }
        public int ActiveClients { get; set; }
        public int InactiveClients { get; set; }
        public int TotalBankUsers { get; set; }
        public int TotalPayments { get; set; }
        public int PendingPayments { get; set; }
        public int ApprovedPayments { get; set; }
        public int RejectedPayments { get; set; }
        public decimal TotalPaymentValue { get; set; }
        public int PendingVerifications { get; set; }
        public int VerifiedClients { get; set; }
        public int RejectedClients { get; set; }
        public int TotalDocuments { get; set; }
        public DateTime? LastActivityDate { get; set; }
    }
}