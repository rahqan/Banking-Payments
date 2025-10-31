using dummy_api.Models.Enums;

namespace dummy_api.Models.DTO
{
    public class LoginRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }


    }
}
