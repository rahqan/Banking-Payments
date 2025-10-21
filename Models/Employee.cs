using System.ComponentModel.DataAnnotations.Schema;

namespace dummy_api.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public ICollection<SalaryDisbursement>? SalaryDisbursements { get; set; }


    }
}
