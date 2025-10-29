using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_Payments.Models
{
    public class ContactDetails
    {
        [Key]
        public int ContactDetailsId { get; set; }

        [Required(ErrorMessage ="Contact Person Name is Required")]
        public string ContactName { get; set; }

        [Required(ErrorMessage ="Contact Number is Requied")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage ="Contact Email is Required")]
        public string ContactEmail { get; set; }

        public bool IsActive { get; set; } = true;

        [ForeignKey("Bank")]
        public int BankId { get; set; }

        public Bank? Bank { get; set; }
    }
}
