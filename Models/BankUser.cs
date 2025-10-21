using System.ComponentModel.DataAnnotations.Schema;

namespace dummy_api.Models
{
    public class BankUser
    {
        public int BankUserId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        [ForeignKey("Bank")]
        public int BankId { get; set; }
        public Bank? Bank { get; set; }
        public ICollection<Client>? Clients { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
