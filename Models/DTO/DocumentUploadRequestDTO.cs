using Microsoft.AspNetCore.Http;

namespace dummy_api.Models.DTO
{
    public class DocumentUploadRequest
    {
        public IFormFile File { get; set; } = null!;
        public int UploadedBy { get; set; }  // BankUserId
        public int ClientId { get; set; }
        public string? Name { get; set; }
    }
}
