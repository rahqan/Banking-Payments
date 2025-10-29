using Banking_Payments.Models.Enums;

namespace Banking_Payments.Models.DTO
{
    public class VerifyClientDTO
    {
        public VerificationStatus VerificationStatus { get; set; }
        public string? Notes { get; set; }
    }
}