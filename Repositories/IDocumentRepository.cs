using Banking_Payments.Models;
namespace Banking_Payments.Repositories
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