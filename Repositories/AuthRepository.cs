using Banking_Payments.Context;
using Banking_Payments.Models;
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
            return await _context.Clients.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Admin?> GetAdminByEmailAsync(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(x => x.Email == email);
        }

        // New registration methods
        public async Task<Admin> CreateAdminAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<BankUser> CreateBankUserAsync(BankUser bankUser)
        {
            await _context.BankUsers.AddAsync(bankUser);
            await _context.SaveChangesAsync();
            return bankUser;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return client;
        }

        // Validation methods
        public async Task<bool> EmailExistsAsync(string email)
        {
            var adminExists = await _context.Admins.AnyAsync(a => a.Email == email);
            var bankUserExists = await _context.BankUsers.AnyAsync(bu => bu.Email == email);
            var clientExists = await _context.Clients.AnyAsync(c => c.Email == email);

            return adminExists || bankUserExists || clientExists;
        }

        public async Task<bool> BankExistsAsync(int bankId)
        {
            return await _context.Banks.AnyAsync(b => b.BankId == bankId);
        }

        public async Task<bool> CodeExistsAsync(string code, string userType)
        {
            return userType switch
            {
                "Admin" => await _context.Admins.AnyAsync(a => a.Code == code),
                "BankUser" => await _context.BankUsers.AnyAsync(bu => bu.Code == code),
                "Client" => await _context.Clients.AnyAsync(c => c.Code == code),
                _ => false
            };
        }

    }
}
