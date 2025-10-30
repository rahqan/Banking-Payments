using Banking_Payments.Models.Enums;

namespace Banking_Payments.Models.DTO
{
    public class LoginRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }


    }
}
