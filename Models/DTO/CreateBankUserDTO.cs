namespace Banking_Payments.Models.DTO
{
    public class CreateBankUserDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int BankId { get; set; }
    }
}
