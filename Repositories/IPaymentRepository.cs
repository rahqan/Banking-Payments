using dummy_api.Models;
using dummy_api.Models.Enums;

namespace dummy_api.Repositories {
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPendingPaymentsByBankIdAsync(int bankId); // New method
        Task<IEnumerable<Payment>> GetPaymentsByStatusAndBankIdAsync(VerificationStatus status, int bankId); // New method
        Task<IEnumerable<Payment>> GetPendingPaymentsAsync(); // Keep for backwards compatibility
        Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(VerificationStatus status); // Keep for backwards compatibility
        Task<IEnumerable<Payment>> GetAllPaymentsByBankIdAsync(int bankId);
        Task<Payment?> GetPaymentByIdAsync(int paymentId);
        Task UpdatePaymentAsync(Payment payment);
        Task SaveChangesAsync();
    }

}