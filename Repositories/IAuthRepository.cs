using Banking_Payments.Models;

namespace Banking_Payments.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<BankUser?> GetBankUserByEmailAsync(string email);
        Task<Client?> GetClientByEmailAsync(string email);
        Task<Admin?> GetAdminByEmailAsync(string email);
    }
}
