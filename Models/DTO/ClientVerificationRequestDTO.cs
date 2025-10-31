using dummy_api.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace dummy_api.Models.DTO
{
    public class ClientVerificationRequestDTO
    {
        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}