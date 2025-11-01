using System.ComponentModel.DataAnnotations;

namespace Banking_Payments.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Bank>? Banks { get; set; }
    }
}
