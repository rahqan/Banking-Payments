using Banking_Payments.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_Payments.Models
{
    public enum PaymentType
    {
        RTGS, IMPS, NEFT
    }
    public class Payment
    {
        public int PaymentId { get; set; }
        public Decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public VerificationStatus status { get; set; } = 0;
        public PaymentType Type { get; set; }
        public string Remarks { get; set; }

        [ForeignKey("Beneficiary")]
        public int BeneficiaryId { get; set; }
        public Beneficiary? Beneficiary { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        [ForeignKey("BankUser")]
        public int? BankUserId { get; set; }
        public BankUser? ApprovedBy { get; set; }
    }
}
