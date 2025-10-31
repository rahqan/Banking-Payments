using dummy_api.Models.Enums;

namespace dummy_api.Models.DTO
{
    public class VerifyClientDTO
    {
        public VerificationStatus VerificationStatus { get; set; }
        public string? Notes { get; set; }
    }
}