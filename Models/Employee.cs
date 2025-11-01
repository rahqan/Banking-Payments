using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_Payments.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }

        public string AccountNumber { get; set; }
        public string IfscCode { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public ICollection<SalaryDisbursement>? SalaryDisbursements { get; set; }
    }
}
