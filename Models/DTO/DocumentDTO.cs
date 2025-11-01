namespace Banking_Payments.Models.DTO
{
    public class DocumentDTO
    {
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int BankUserId { get; set; }
        public string? UploadedByName { get; set; } // Optional: Bank user's name
        public int ClientId { get; set; }
        public string? ClientName { get; set; } // Optional: Client's name
        public DateTime UploadedAt { get; set; }
        public string? DocType { get; set; } // Document type/category
    }
}
