using Banking_Payments.Models;
using Banking_Payments.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking_Payments.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "BankUser")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IDocumentService documentService,ILogger<DocumentController> logger)
        {
            _logger = logger;
            _documentService = documentService;
        }

        // Helper to extract user info from JWT
        private int GetBankId()
        {
            var bankIdClaim = User.FindFirst("BankId")?.Value;
            if (string.IsNullOrEmpty(bankIdClaim))
                throw new UnauthorizedAccessException("BankId claim missing.");
            return int.Parse(bankIdClaim);
        }

        private int GetBankUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("UserId claim missing.");
            return int.Parse(userIdClaim);
        }

        // ---------- Upload ----------
        //[HttpPost("upload")]
        //public async Task<IActionResult> UploadDocument(IFormFile file, int clientId, string? docType)
        //    [HttpPost("upload")]
        //    public async Task<IActionResult> UploadDocument(
        //[FromForm] IFormFile file,
        //[FromForm] int clientId,
        //[FromForm] string? docType)

        //    {

        //        _logger.LogInformation("UploadDocument called with clientId: {ClientId}, docType: {DocType}", clientId, docType);
        //        if (file == null || file.Length == 0)
        //            return BadRequest(new { message = "File is required." });

        //        var bankId = GetBankId();
        //        var bankUserId = GetBankUserId();

        //        var result = await _documentService.UploadDocumentAsync(file, bankUserId, bankId, clientId, docType);

        //        if (result == null)
        //            return BadRequest(new { message = "Failed to upload document." });

        //        return Ok(new { message = "Document uploaded successfully.", data = result });
        //    }



        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadDocument([FromForm] UploadDocumentRequest request)
        {
            _logger.LogInformation("UploadDocument called with clientId: {ClientId}, docType: {DocType}", request.ClientId, request.DocType);

            var bankId = GetBankId();
            var bankUserId = GetBankUserId();

            var result = await _documentService.UploadDocumentAsync(
                request.File, bankUserId, bankId, request.ClientId, request.DocType
            );

            if (result == null)
                return BadRequest(new { message = "Failed to upload document." });

            return Ok(new { message = "Document uploaded successfully.", data = result });
        }


        // ---------- Get by ID ----------
        [HttpGet("{documentId:int}")]
        public async Task<IActionResult> GetDocumentById(int documentId)
        {
            var bankId = GetBankId();
            var result = await _documentService.GetDocumentByIdAsync(documentId, bankId);

            if (result == null)
                return NotFound(new { message = "Document not found or access denied." });

            return Ok(new { data = result });
        }

        // ---------- Get by Client ----------
        [HttpGet("client/{clientId:int}")]
        public async Task<IActionResult> GetDocumentsByClient(int clientId)
        {
            var bankId = GetBankId();
            var result = await _documentService.GetDocumentsByClientIdAsync(clientId, bankId);

            return Ok(new { data = result, count = result.Count });
        }

        // ---------- Delete ----------
        [HttpDelete("{documentId:int}")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            var bankId = GetBankId();
            var result = await _documentService.DeleteDocumentAsync(documentId, bankId);

            if (!result)
                return NotFound(new { message = "Document not found or access denied." });

            return Ok(new { message = "Document deleted successfully." });
        }
    }
}
