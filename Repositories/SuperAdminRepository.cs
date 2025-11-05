using Banking_Payments.Context;
using Banking_Payments.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking_Payments.Repositories
{
    public class SuperAdminRepository : ISuperAdminRepository
    {
        private readonly AppDbContext _context;
        public SuperAdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BankUser>> GetAllBankUsersAsync()
        {
            return await _context.BankUsers.Include(bu => bu.Bank).ToListAsync();
        }

        public async Task<BankUser?> GetBankUserByIdAsync(int id)
        {
            return await _context.BankUsers.Include(bu => bu.Bank)
                                           .FirstOrDefaultAsync(bu => bu.BankUserId == id);
        }

        public async Task AddBankUserAsync(BankUser user)
        {
            await _context.BankUsers.AddAsync(user);
        }

        public async Task UpdateBankUserAsync(BankUser user)
        {
            _context.BankUsers.Update(user);
        }

        public async Task DeleteBankUserAsync(int id)
        {
            var user = await _context.BankUsers.FindAsync(id);
            if (user != null)
                _context.BankUsers.Remove(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
