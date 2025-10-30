using Banking_Payments.Models.Enums;

namespace Banking_Payments.Models.DTO
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public Role Role { get; set; }
        public int UserId { get; set; }
    }
}
