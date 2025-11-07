using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_Payments.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public string Address { get; set; }
        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public ICollection<SalaryDisbursement>? SalaryDisbursements { get; set; }
    }
}