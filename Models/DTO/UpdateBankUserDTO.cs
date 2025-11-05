namespace Banking_Payments.Models.DTO
{
    public class UpdateBankUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool? ResetPassword { get; set; } = false;
        public string? NewPassword { get; set; }
    }
}
