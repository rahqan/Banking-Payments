using Banking_Payments.Context;
using Banking_Payments.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking_Payments.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly AppDbContext _context;

        public BankRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bank>> GetAllAsync()
        {
            return await _context.Banks
                .Where(b => b.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Bank?> GetByIdAsync(int id)
        {
            return await _context.Banks.FirstOrDefaultAsync(b => b.BankId == id);
        }

        public async Task<Bank> AddAsync(Bank bank)
        {
            await _context.Banks.AddAsync(bank);
            return bank;
        }

        public async Task UpdateAsync(Bank bank)
        {
            _context.Banks.Update(bank);
        }

        public async Task SoftDeleteAsync(int id)
        {
            var bank = await _context.Banks.FirstOrDefaultAsync(b => b.BankId == id);
            if (bank != null)
            {
                bank.IsActive = false;
                _context.Banks.Update(bank);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
