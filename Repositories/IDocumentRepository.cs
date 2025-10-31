using dummy_api.Models;
namespace dummy_api.Repositories
{
    public interface IDocumentRepository
    {
        Task<Document?> GetByIdAsync(int id);
        Task<List<Document>> GetByClientIdAsync(int clientId, int bankId);
        Task AddAsync(Document document);
        Task DeleteAsync(Document document);
        Task SaveChangesAsync();
    }
}