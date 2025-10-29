using Banking_Payments.Models.Enums;
using System.ComponentModel.DataAnnotations;

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