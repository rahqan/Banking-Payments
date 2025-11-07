using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_Payments.Models
{
    public class Beneficiary
    {
        public int BeneficiaryId { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
        public string BankName { get; set; }
        public string RelationShip {  get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }

        public ICollection<Payment>? Payments { get; set; }
    }
}
