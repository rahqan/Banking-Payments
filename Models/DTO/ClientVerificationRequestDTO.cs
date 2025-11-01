using System.ComponentModel.DataAnnotations;
using Banking_Payments.Models.Enums;

namespace Banking_Payments.Models.DTO
{
    public class ClientVerificationRequestDTO
    {
        [Required]
        public VerificationStatus VerificationStatus { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
