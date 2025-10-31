using dummy_api.Context;
using dummy_api.Models;
using dummy_api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace dummy_api.Repositories
{
    public class BankUserRepository : IBankUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public BankUserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Client> CreateClientAsync(Client clientModel)
        {
            if (clientModel == null)
                throw new ArgumentNullException(nameof(clientModel));

            await _appDbContext.Clients.AddAsync(clientModel);
            await _appDbContext.SaveChangesAsync();
            return clientModel;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync(int bankId)
        {
            return await _appDbContext.Clients
                .Where(c => c.BankId == bankId && c.IsActive)
                .Include(c => c.Employees)
                .Include(c => c.Beneficiaries)
                .Include(c => c.Payments)
                .ToListAsync();
        }

        public async Task<Client?> GetClientByIdAsync(int clientId)
        {
            return await _appDbContext.Clients
                .Include(c => c.Employees)
                .Include(c => c.Beneficiaries)
                .Include(c => c.Payments)
                .FirstOrDefaultAsync(c => c.ClientId == clientId && c.IsActive);
        }

        public async Task<bool> UpdateClientAsync(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            _appDbContext.Clients.Update(client);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteClientAsync(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            // Soft delete
            client.IsActive = false;
            _appDbContext.Clients.Update(client);
            var result = await _appDbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Client>> GetClientsByVerificationStatusAsync(string status, int bankId)
        {
            return await _appDbContext.Clients
                .Where(c => c.VerificationStatus == status && c.BankId == bankId && c.IsActive)
                .Include(c => c.Employees)
                .Include(c => c.Beneficiaries)
                .Include(c => c.Payments)
                .ToListAsync();
        }

        public async Task<Bank?> GetBankByIdAsync(int bankId)
        {
            return await _appDbContext.Banks
                .FirstOrDefaultAsync(b => b.BankId == bankId);
        }

        public async Task<int> GetClientCountByBankIdAsync(int bankId)
        {
            return await _appDbContext.Clients
                .CountAsync(c => c.BankId == bankId);
        }

        public async Task<Client?> GetClientByCodeAsync(string clientCode)
        {
            return await _appDbContext.Clients
                .FirstOrDefaultAsync(c => c.Code == clientCode && c.IsActive);
        }
    }
}