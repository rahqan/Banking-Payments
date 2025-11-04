namespace Banking_Payments.Models.DTO
{
    public class BeneficiaryDTO
    {
        public int BeneficiaryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string IfscCode { get; set; } = string.Empty;
    }
}
