using Banking_Payments.Context;
using Banking_Payments.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;


namespace Banking_Payments.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _context;

        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Document?> GetByIdAsync(int id)
        {
            return await _context.Documents
                        .Include(d => d.UploadedBy)
                        .Include(d => d.Client)
                        .FirstOrDefaultAsync(d => d.DocumentId == id);
        }

        public async Task<List<Document>> GetByClientIdAsync(int clientId, int bankId)
        {
            return await _context.Documents
                .Include(d => d.UploadedBy)
                .Include(d => d.Client)
                .Where(d => d.ClientId == clientId && d.UploadedBy != null && d.UploadedBy.BankId == bankId)
                .ToListAsync();
        }

        public async Task AddAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
        }

        public async Task DeleteAsync(Document document)
        {
            _context.Documents.Remove(document);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
