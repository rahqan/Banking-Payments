using Banking_Payments.Models;
using Banking_Payments.Models.Enums;

namespace Banking_Payments.Repositories
{
    public interface IBankUserRepository
    {
        Task<Client> CreateClientAsync(Client clientModel);
        Task<IEnumerable<Client>> GetAllClientsAsync(int bankId);
        Task<Client?> GetClientByIdAsync(int clientId);
        Task<bool> UpdateClientAsync(Client client);
        Task<bool> DeleteClientAsync(Client client);
        Task<IEnumerable<Client>> GetClientsByVerificationStatusAsync(VerificationStatus status, int bankId);
        Task<Bank?> GetBankByIdAsync(int bankId);
        Task<int> GetClientCountByBankIdAsync(int bankId);
        Task<Client?> GetClientByCodeAsync(string clientCode);
    }
}