using System.ComponentModel.DataAnnotations.Schema;

namespace Banking_Payments.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        [ForeignKey("BankUser")]
        public int BankUserId { get; set; }
        public BankUser? UploadedBy { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        //public Client? Client { get; set; } = new Client();
        public Client? Client { get; set; }

    }
}
