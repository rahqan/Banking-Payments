using dummy_api.Models.DTO;
using dummy_api.Models;
using dummy_api.Repositories;
using dummy_api.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace Banking_Payments.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IDocumentRepository _repository;
        private readonly ILogger<DocumentService> _logger;

        private static readonly string[] VideoExtensions =
            { ".mp4", ".mov", ".avi", ".mkv", ".webm", ".mpeg", ".mpg", ".m4v" };

        public DocumentService(
            IOptions<CloudinarySettings> cloudSettings,
            IDocumentRepository repository,
            ILogger<DocumentService> logger)
        {
            _repository = repository;
            _logger = logger;

            var account = new Account(
                cloudSettings.Value.CloudName,
                cloudSettings.Value.ApiKey,
                cloudSettings.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        // ---------- Upload ----------
        public async Task<DocumentDTO?> UploadDocumentAsync(
            IFormFile file,
            int bankUserId,
            int bankId,
            int clientId,
            string? docType = null)
        {
            try
            {
                _logger.LogInformation("Starting document upload for file: {FileName}", file.FileName);

                var uploadResult = await UploadToCloudinary(file);

                if (uploadResult == null || uploadResult.Error != null)
                {
                    var msg = uploadResult?.Error?.Message ?? "Upload failed";
                    _logger.LogError("Cloudinary upload failed: {Error}", msg);
                    return null;
                }

                // Log the generated URL for debugging
                var documentUrl = uploadResult.SecureUrl?.ToString() ?? uploadResult.Url?.ToString();
                _logger.LogInformation("Cloudinary upload successful. URL: {Url}, PublicId: {PublicId}",
                    documentUrl, uploadResult.PublicId);

                var document = new Document
                {
                    Name = file.FileName,
                    Url = documentUrl,
                    BankUserId = bankUserId,
                    ClientId = clientId,
                    DocType = docType,
                    UploadedAt = DateTime.UtcNow
                };

                _logger.LogInformation("Adding document to repository");
                await _repository.AddAsync(document);
                await _repository.SaveChangesAsync();

                _logger.LogInformation("Document saved successfully with ID: {DocumentId}, URL: {Url}",
                    document.DocumentId, document.Url);

                return MapToDTO(document);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading document. FileName: {FileName}, ClientId: {ClientId}, BankUserId: {BankUserId}",
                    file.FileName, clientId, bankUserId);
                throw;
            }
        }



        private async Task<UploadResult> UploadToCloudinary(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            // Copy to a MemoryStream so Cloudinary reads full content safely
            await using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0; // reset pointer!

            var fileDesc = new FileDescription(file.FileName, stream);
            var ext = Path.GetExtension(file.FileName)?.ToLowerInvariant() ?? "";

            UploadResult result;

            if (file.ContentType.StartsWith("image/"))
            {
                var uploadParams = new ImageUploadParams
                {
                    File = fileDesc,
                    Folder = "documents/images",
                    PublicId = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.UtcNow.Ticks}",
                    Overwrite = false
                };
                result = await _cloudinary.UploadAsync(uploadParams);
            }
            else if (file.ContentType.StartsWith("video/") || VideoExtensions.Contains(ext))
            {
                var uploadParams = new VideoUploadParams
                {
                    File = fileDesc,
                    Folder = "documents/videos",
                    PublicId = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.UtcNow.Ticks}",
                    Overwrite = false
                };
                result = await _cloudinary.UploadAsync(uploadParams);
            }
            else
            {
                // For PDFs and other files, use raw upload explicitly
                var uploadParams = new RawUploadParams
                {
                    File = fileDesc,
                    Folder = "documents/files",
                    PublicId = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{DateTime.UtcNow.Ticks}",
                    Overwrite = false
                };
                result = await _cloudinary.UploadAsync(uploadParams);
            }

            _logger.LogInformation("Upload Result - StatusCode: {StatusCode}, PublicId: {PublicId}, URL: {Url}",
                result.StatusCode, result.PublicId, result.SecureUrl ?? result.Url);

            if (result.Error != null)
            {
                _logger.LogError("Cloudinary Error: {ErrorMessage}", result.Error.Message);
            }

            return result;
        }



        // ---------- Get by DocumentId ----------
        public async Task<DocumentDTO?> GetDocumentByIdAsync(int documentId, int bankId)
        {
            try
            {
                var document = await _repository.GetByIdAsync(documentId);

                if (document == null)
                {
                    _logger.LogWarning("Document {DocumentId} not found", documentId);
                    return null;
                }

                if (document.UploadedBy?.BankId != bankId)
                {
                    _logger.LogWarning("Access denied for document {DocumentId}. Bank {BankId} does not own this document",
                        documentId, bankId);
                    return null;
                }

                _logger.LogInformation("Retrieved document {DocumentId} with URL: {Url}", documentId, document.Url);
                return MapToDTO(document);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving document {DocumentId}", documentId);
                return null;
            }
        }

        // ---------- Get by ClientId ----------
        public async Task<List<DocumentDTO>> GetDocumentsByClientIdAsync(int clientId, int bankId)
        {
            try
            {
                var docs = await _repository.GetByClientIdAsync(clientId, bankId);

                if (docs == null || !docs.Any())
                    return new List<DocumentDTO>();

                return docs.Select(MapToDTO).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving documents for client {ClientId}", clientId);
                return new List<DocumentDTO>();
            }
        }

        // ---------- Delete ----------
        public async Task<bool> DeleteDocumentAsync(int documentId, int bankId)
        {
            try
            {
                var document = await _repository.GetByIdAsync(documentId);

                if (document == null)
                    return false;

                if (document.UploadedBy?.BankId != bankId)
                    return false;

                // Optional: Delete from Cloudinary as well
                // Extract public ID from URL and delete
                // var publicId = ExtractPublicIdFromUrl(document.Url);
                // await _cloudinary.DestroyAsync(new DeletionParams(publicId));

                await _repository.DeleteAsync(document);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting document {DocumentId}", documentId);
                return false;
            }
        }

        // ---------- Mapping Method ----------
        private DocumentDTO MapToDTO(Document document)
        {
            return new DocumentDTO
            {
                DocumentId = document.DocumentId,
                Name = document.Name,
                Url = document.Url,
                BankUserId = document.BankUserId,
                UploadedByName = document.UploadedBy?.Name,
                ClientId = document.ClientId,
                ClientName = document.Client?.Name,
                UploadedAt = document.UploadedAt,
                DocType = document.DocType
            };
        }
    }
}