namespace Banking_Payments.Models.DTO
{
    public class BankUserDTO
    {
        public int BankUserId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int BankId { get; set; }
    }
}
