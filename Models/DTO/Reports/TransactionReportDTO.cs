namespace Banking_Payments.Models.DTO.Reports
{
    public class TransactionReportDTO
    {
        public int PaymentId { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
        public string ClientName { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string ApprovedByName { get; set; }
        public string Remarks { get; set; }
    }
}