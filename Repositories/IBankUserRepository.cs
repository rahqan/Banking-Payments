using Banking_Payments.Models;
using Banking_Payments.Models.DTO;

namespace Banking_Payments.Repositories
{
    public interface IBankUserRepository
    {
        Task<Client> CreateClientAsync(Client client);
        Task<PagedResult<ClientDTO>> GetAllClientsAsync(int bankId,int pageNumber,int pageSize,
     string? status = null,
     string? searchTerm = null);
        Task<Client?> GetClientByIdAsync(int clientId);
        Task<bool> UpdateClientAsync(Client client);
        Task<bool> DeleteClientAsync(Client client);
        Task<IEnumerable<Client>> GetClientsByVerificationStatusAsync(string status, int bankId);
        Task<Bank?> GetBankByIdAsync(int bankId);
        Task<int> GetClientCountByBankIdAsync(int bankId);
        Task<Client?> GetClientByCodeAsync(string clientCode);
        Task<ClientStatsDTO> GetClientStatsAsync(int bankId);

        //Task<IEnumerable<Client>> GetClientsByVerificationStatusAsync(strinb verificationStatus, int bankId);
    }
}
