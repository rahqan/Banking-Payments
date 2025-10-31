using dummy_api.Context;
using dummy_api.Models;
using dummy_api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace dummy_api.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        // CRITICAL: New method - Get pending payments filtered by bank
        public async Task<IEnumerable<Payment>> GetPendingPaymentsByBankIdAsync(int bankId)
        {
            return await _context.Payments
                .Include(p => p.Client)
                .Include(p => p.ApprovedBy)
                .Include(p => p.Beneficiary)
                .Where(p => p.status == VerificationStatus.Pending && p.Client.BankId == bankId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        // CRITICAL: New method - Get payments by status filtered by bank
        public async Task<IEnumerable<Payment>> GetPaymentsByStatusAndBankIdAsync(VerificationStatus status, int bankId)
        {
            return await _context.Payments
                .Include(p => p.Client)
                .Include(p => p.ApprovedBy)
                .Include(p => p.Beneficiary)
                .Where(p => p.status == status && p.Client.BankId == bankId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        // Keep old methods for backwards compatibility if needed elsewhere
        public async Task<IEnumerable<Payment>> GetPendingPaymentsAsync()
        {
            return await _context.Payments
                .Include(p => p.Client)
                .Include(p => p.ApprovedBy)
                .Include(p => p.Beneficiary)
                .Where(p => p.status == VerificationStatus.Pending)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(VerificationStatus status)
        {
            return await _context.Payments
                .Include(p => p.Client)
                .Include(p => p.ApprovedBy)
                .Include(p => p.Beneficiary)
                .Where(p => p.status == status)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsByBankIdAsync(int bankId)
        {
            return await _context.Payments
                .Include(p => p.Client)
                .Include(p => p.ApprovedBy)
                .Include(p => p.Beneficiary)
                .Where(p => p.Client.BankId == bankId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Payments
                .Include(p => p.Client)
                .Include(p => p.ApprovedBy)
                .Include(p => p.Beneficiary)
                .FirstOrDefaultAsync(p => p.PaymentId == paymentId);
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}