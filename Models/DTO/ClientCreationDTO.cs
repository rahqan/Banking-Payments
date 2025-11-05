namespace Banking_Payments.Models.DTO
{
    public class ClientCreationDTO
    {
        public string ClientName { get; set; }
        public string ClientCode { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPassword { get; set; }
        public string RegisterationNumber { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
        public double Balance { get; set; }
        public int BankId { get; set; }
        public int BankUserId { get; set; }
        public string? Address { get; set; }
        public string? ClientBusinessType { get; set; }
    }
}
