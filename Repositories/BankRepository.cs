using Banking_Payments.Context;
using Banking_Payments.Models;
using Banking_Payments.Models.DTO;
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

        public async Task<List<BankWithClientsDTO>> GetAllWithClientCountAsync()
        {
            return await _context.Banks
                .Select(b => new BankWithClientsDTO
                {
                    BankId = b.BankId,
                    Code = b.Code,
                    Name = b.Name,
                    Address = b.Address,
                    PanNumber = b.PanNumber,
                    RegistrationNumber = b.RegistrationNumber,
                    ContactEmail = b.ContactEmail,
                    ContactPhone = b.ContactPhone,
                    IsActive = b.IsActive,
                    CreatedAt = b.CreatedAt,
                    CreatedByAdminId = b.CreatedByAdminId,
                    ClientCount = b.Clients != null ? b.Clients.Count : 0
                })
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
