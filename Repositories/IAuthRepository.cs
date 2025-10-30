using Banking_Payments.Models;

namespace Banking_Payments.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<BankUser?> GetBankUserByEmailAsync(string email);
        Task<Client?> GetClientByEmailAsync(string email);
        Task<Admin?> GetAdminByEmailAsync(string email);




        // New registration methods
        Task<Admin> CreateAdminAsync(Admin admin);
        Task<BankUser> CreateBankUserAsync(BankUser bankUser);
        Task<Client> CreateClientAsync(Client client);

        // Validation methods
        Task<bool> EmailExistsAsync(string email);
        Task<bool> BankExistsAsync(int bankId);
        Task<bool> CodeExistsAsync(string code, string userType);

    }
}
