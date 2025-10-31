using dummy_api.Models.DTO;

namespace dummy_api.Services
{
    public interface IDocumentService
    {
        Task<DocumentDTO?> UploadDocumentAsync(
            IFormFile file,
            int bankUserId,
            int bankId,
            int clientId,
            string? docType = null);

        Task<DocumentDTO?> GetDocumentByIdAsync(int documentId, int bankId);

        Task<List<DocumentDTO>> GetDocumentsByClientIdAsync(int clientId, int bankId);

        Task<bool> DeleteDocumentAsync(int documentId, int bankId);
    }
}