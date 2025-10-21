using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dummy_api.Models
{
    public class Bank
    {
        [Key]
        public int BankId { get; set; }

        public string Code { get; set; }

        [Required(ErrorMessage ="Bank Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Bank Headquarter Address is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Bank PAN number is Must")]
        public string PanNumber { get; set; }

        [Required(ErrorMessage ="Registration Number is Required")]
        public string RegistrationNumber { get; set; }

        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("Admin")]
        public int AdminId { get; set; }

        public virtual Admin? Admin { get; set; }
        public virtual ICollection<Client>? Clients { get; set; }
        public virtual ICollection<BankUser>? BankUsers { get; set; }
    }
}
