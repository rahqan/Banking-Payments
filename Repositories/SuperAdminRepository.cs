using Banking_Payments.Context;
using Banking_Payments.Models;
using Banking_Payments.Models.DTO;
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

        public async Task<IEnumerable<BankUserDTO>> GetBankUsersByBankIdAsync(int bankId)
        {
            var users = await _context.BankUsers
                .Where(u => u.BankId == bankId)
                .Select(u => new BankUserDTO
                {
                    BankUserId = u.BankUserId,
                    Name = u.Name,
                    Email = u.Email,
                    Code = u.Code,
                    BankId = u.BankId
                })
                .ToListAsync();

            return users;
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
