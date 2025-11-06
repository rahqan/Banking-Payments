namespace Banking_Payments.Models.DTO.Reports
{
    public class TransactionVolumeReportDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalPayments { get; set; }
        public decimal TotalPaymentAmount { get; set; }
        public int TotalSalaryDisbursements { get; set; }
        public decimal TotalSalaryAmount { get; set; }
        public PaymentTypeBreakdown PaymentTypeBreakdown { get; set; }
        public PaymentStatusBreakdown PaymentStatusBreakdown { get; set; }
        public List<BankTransactionSummary> BankWiseTransactions { get; set; }
        public List<DailyTransactionTrend> DailyTrends { get; set; }
    }

    public class PaymentTypeBreakdown
    {
        public int RTGSCount { get; set; }
        public decimal RTGSAmount { get; set; }
        public int IMPSCount { get; set; }
        public decimal IMPSAmount { get; set; }
        public int NEFTCount { get; set; }
        public decimal NEFTAmount { get; set; }
    }

    public class PaymentStatusBreakdown
    {
        public int PendingCount { get; set; }
        public decimal PendingAmount { get; set; }
        public int ApprovedCount { get; set; }
        public decimal ApprovedAmount { get; set; }
        public int RejectedCount { get; set; }
        public decimal RejectedAmount { get; set; }
    }

    public class BankTransactionSummary
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public int TransactionCount { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class DailyTransactionTrend
    {
        public DateTime Date { get; set; }
        public int TransactionCount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}