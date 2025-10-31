using System.ComponentModel.DataAnnotations.Schema;

namespace dummy_api.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        [ForeignKey("BankUser")]
        public int BankUserId { get; set; }
        public BankUser? UploadedBy { get; set; } = new BankUser();

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string? DocType { get; set; } // e.g., "ID", "Proof of Address", etc.
    }
}