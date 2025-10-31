using dummy_api.Models.Enums;

namespace dummy_api.Models.DTO
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public Role Role { get; set; }
        public int UserId { get; set; }
    }
}
