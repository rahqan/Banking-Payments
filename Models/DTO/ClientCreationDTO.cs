namespace Banking_Payments.Models.DTO
{
    public class ClientCreationDTO
    {
        public string ClientName { get; set; }

        //public string ClientCode { get; set; }
        public string ClientEmail { get; set; }
        public int BankId { get; set; }
        public int BankUserId { get; set; }
        public string? Address { get; set; }
        public string? ClientBusinessType { get; set; }
    }
}
