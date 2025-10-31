using dummy_api.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dummy_api.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string BusinessType { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string RegisterationNumber { get; set; }
        public string VerificationStatus { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }

        [ForeignKey("Bank")]
        public int BankId { get; set; }
        public virtual Bank? Bank { get; set; }

        [ForeignKey("BankUser")]
        public int BankUserId { get; set; }
        public virtual BankUser? BankUser { get; set; } = new BankUser();
        public virtual ICollection<Employee>? Employees { get; set; }
        public virtual ICollection<SalaryDisbursement>? SalaryDisbursement { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
        public virtual ICollection<Beneficiary>? Beneficiaries { get; set; }
        public virtual ICollection<Document>? Documents { get; set; }

    }
}