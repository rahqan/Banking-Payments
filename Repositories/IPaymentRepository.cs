//using Banking_Payments.Models;
//using Banking_Payments.Models.Enums;

//namespace Banking_Payments.Repositories
//{
//    public interface IPaymentRepository
//    {
//        Task<IEnumerable<Payment>> GetPendingPaymentsAsync();
//        Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(VerificationStatus status);
//        Task<IEnumerable<Payment>> GetAllPaymentsByBankUserIdAsync(long bankId);
//        Task<Payment?> GetPaymentByIdAsync(long paymentId);
//        Task UpdatePaymentAsync(Payment payment);
//        Task SaveChangesAsync();
//    }
//}


using Banking_Payments.Models;
using Banking_Payments.Models.Enums;

namespace Banking_Payments.Repositories {
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