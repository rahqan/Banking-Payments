using Banking_Payments.Models;

namespace Banking_Payments.Repositories
{
    public interface IBankRepository
    {
        Task<IEnumerable<Bank>> GetAllAsync();
        Task<Bank?> GetByIdAsync(int id);
        Task<Bank> AddAsync(Bank bank);
        Task UpdateAsync(Bank bank);
        Task SoftDeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
