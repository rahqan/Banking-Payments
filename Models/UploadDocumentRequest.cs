using Microsoft.AspNetCore.Mvc;

namespace Banking_Payments.Models
{
    public class UploadDocumentRequest
    {
        [FromForm] public IFormFile File { get; set; } = default!;
        [FromForm] public int ClientId { get; set; }
        [FromForm] public string? DocType { get; set; }
    }

}
