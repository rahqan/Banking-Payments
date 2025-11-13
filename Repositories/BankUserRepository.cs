using Banking_Payments.Context;
using Banking_Payments.Models;
using Banking_Payments.Models.DTO;
using Banking_Payments.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Banking_Payments.Repositories
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


        //ClientStatsDTO IBankUserRepository.GetClientStatsAsync(int bankId)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<ClientStatsDTO> GetClientStatsAsync(int bankId)
        {
            var allClients = await _appDbContext.Clients.Where(c => c.BankId == bankId).ToListAsync();

            return new ClientStatsDTO
            {
                TotalClients = allClients.Count,
                PendingOnboard = allClients.Count(c => c.VerificationStatus == VerificationStatus.Pending.ToString()),
                VerifiedClients = allClients.Count(c => c.VerificationStatus == VerificationStatus.Verified.ToString()),
                RejectedClients = allClients.Count(c => c.VerificationStatus == VerificationStatus.Rejected.ToString())
            };
        }


        //Task<PagedResult<Client>> IBankUserRepository.GetAllClientsAsync(int bankId, int pageNumber, int pageSize, string? status, string? searchTerm)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ClientStatsDTO> GetClientStatsAsync(int bankId, string? status = null, string? searchTerm = null)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<PagedResult<ClientDTO>> GetAllClientsAsync(
     int bankId,
     int pageNumber,
     int pageSize,
     string? status = null,
     string? searchTerm = null)
        {
            var query = _appDbContext.Clients.Where(c => c.BankId == bankId);

            // Apply status filter
            if (!string.IsNullOrEmpty(status) && status != "All")
            {
                if (Enum.TryParse<VerificationStatus>(status, out var verificationStatus))
                {
                    query = query.Where(c => c.VerificationStatus == verificationStatus.ToString());
                }
            }

            // Apply search filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerSearchTerm = searchTerm.ToLower();
                query = query.Where(c =>
                    c.Name.ToLower().Contains(lowerSearchTerm) ||
                    c.Email.ToLower().Contains(lowerSearchTerm));
            }

            // Get total count after filtering
            var totalRecords = await query.CountAsync();

            // Apply pagination
            var clients = await query
                .OrderByDescending(c => c.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var clientDTOs = clients.Select(c => new ClientDTO
            {
                ClientId = c.ClientId,
                ClientName = c.Name,
                ClientEmail = c.Email,
                ClientBusinessType = c.BusinessType,
                ClientVerificationStatus = c.VerificationStatus.ToString(),
                CreatedAt = c.CreatedAt
            }).ToList();

            return new PagedResult<ClientDTO>
            {
                Data = clientDTOs,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
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
