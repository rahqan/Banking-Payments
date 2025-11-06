namespace Banking_Payments.Models.DTO.Reports
{
    public class PaymentApprovalReportDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalPayments { get; set; }
        public int PendingApprovals { get; set; }
        public int ApprovedPayments { get; set; }
        public int RejectedPayments { get; set; }
        public decimal TotalApprovedAmount { get; set; }
        public decimal TotalRejectedAmount { get; set; }
        public double AverageApprovalTimeHours { get; set; }
        public List<BankUserApprovalPerformance> BankUserPerformance { get; set; }
        public List<HighValueTransaction> HighValueTransactions { get; set; }
    }

    public class BankUserApprovalPerformance
    {
        public int BankUserId { get; set; }
        public string BankUserName { get; set; }
        public int TotalApproved { get; set; }
        public int TotalRejected { get; set; }
        public decimal TotalAmountApproved { get; set; }
    }

    public class HighValueTransaction
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string ClientName { get; set; }
        public string BeneficiaryName { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
    }
}
