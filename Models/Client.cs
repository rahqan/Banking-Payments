using Banking_Payments.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_Payments.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }


        public string Password { get; set; }
        public string ClientCode { get; set; }



        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientBusinessType { get; set; }
        public string ClientAddress { get; set; }
        public VerificationStatus ClientVerificationStatus { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("Bank")]
        public int BankId { get; set; }
        public virtual Bank? Bank { get; set; }

        [ForeignKey("BankUser")]
        public int BankUserId { get; set; }

        public virtual BankUser? BankUser { get; set; }

        // public virtual BankUser? BankUser { get; set; } = new BankUser();
        public virtual ICollection<Employee>? Employees { get; set; }
        public virtual ICollection<SalaryDisbursement>? SalaryDisbursement { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
        public virtual ICollection<Beneficiary>? Beneficiaries { get; set; }
        public virtual ICollection<Document>? Documents { get; set; }

    }
}