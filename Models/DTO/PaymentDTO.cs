using dummy_api.Models;
using dummy_api.Models.Enums;

namespace dummy_api.Models.DTO
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public VerificationStatus Status { get; set; }
        public PaymentType Type { get; set; }
        public int BeneficiaryId { get; set; }
        public string? BeneficiaryName { get; set; }
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public int? ApprovedById { get; set; }
        public string? ApprovedByName { get; set; }
    }
}
