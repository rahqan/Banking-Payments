using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_Payments.Models
{
    public class Beneficiary
    {
        public int BeneficiaryId { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public virtual Client? Client { get; set; }
    }
}