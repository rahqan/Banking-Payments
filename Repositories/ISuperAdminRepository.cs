using Banking_Payments.Models;
using Banking_Payments.Models.DTO;

namespace Banking_Payments.Repositories
{
    public interface ISuperAdminRepository
    {
        // BANK USERS CRUD
        Task<IEnumerable<BankUser>> GetAllBankUsersAsync();
        Task<BankUser?> GetBankUserByIdAsync(int id);
        Task AddBankUserAsync(BankUser user);
        Task UpdateBankUserAsync(BankUser user);
        Task DeleteBankUserAsync(int id);
        Task SaveChangesAsync();

        Task<IEnumerable<BankUserDTO>> GetBankUsersByBankIdAsync(int bankId);
    }
}
