using System.ComponentModel.DataAnnotations.Schema;

namespace dummy_api.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string Name { get; set; }
        [ForeignKey("BankUser")]
        public int BankUserId { get; set; }
        public BankUser? UploadedBy { get; set; }

    }
}
