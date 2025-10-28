using System.ComponentModel.DataAnnotations.Schema;

namespace dummy_api.Models
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
        public int status { get; set; } = 0;

        public PaymentType Type { get; set; }

        public int BeneficiaryId { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        [ForeignKey("BankUser")]
        public int BankUserId { get; set; }
        public BankUser? ApprovedBy { get; set; }
    }
}
