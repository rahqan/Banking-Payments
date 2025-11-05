namespace Banking_Payments.Models.DTO
{
    public class ClientBankDetailsDTO
    {
        public string accountHolder { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public double Balance { get; set; }
        public string IfscCode { get; set; }
        public string AccountType { get; set; } = "Current Account";
        public string Branch {  get; set; }
    }
}
