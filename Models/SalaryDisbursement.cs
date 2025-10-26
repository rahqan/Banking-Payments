using System.ComponentModel.DataAnnotations.Schema;

namespace dummy_api.Models
{
    public class SalaryDisbursement
    {
        public int SalaryDisbursementId { get; set; }
        public DateTime CreatedAt {  get; set; }
        public decimal Amount { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }
    }
}
