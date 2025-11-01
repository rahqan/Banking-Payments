using Banking_Payments.Models;

namespace Banking_Payments.Repositories
{
    public interface IBankUserRepository
    {
        Task<Client> CreateClientAsync(Client clientModel);
        Task<IEnumerable<Client>> GetAllClientsAsync(int bankId);
        Task<Client?> GetClientByIdAsync(int clientId);
        Task<bool> UpdateClientAsync(Client client);
        Task<bool> DeleteClientAsync(Client client);
        Task<IEnumerable<Client>> GetClientsByVerificationStatusAsync(string status, int bankId);
        Task<Bank?> GetBankByIdAsync(int bankId);
        Task<int> GetClientCountByBankIdAsync(int bankId);
        Task<Client?> GetClientByCodeAsync(string clientCode);
        //Task<IEnumerable<Client>> GetClientsByVerificationStatusAsync(strinb verificationStatus, int bankId);
    }
}
