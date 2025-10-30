using Banking_Payments.Context;
using Banking_Payments.Models;
using Banking_Payments.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Banking_Payments.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BankUser?> GetBankUserByEmailAsync(string email)
        {
            return await _context.BankUsers.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Client?> GetClientByEmailAsync(string email)
        {
            return await _context.Clients.FirstOrDefaultAsync(x => x.ClientEmail == email);
        }

        public async Task<Admin?> GetAdminByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
