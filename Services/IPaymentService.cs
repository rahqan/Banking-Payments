using dummy_api.Models.DTO;
using dummy_api.Models;
using dummy_api.Models.Enums;

namespace dummy_api.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDTO>> GetPendingPaymentsAsync(int bankId); // Added bankId
        Task<IEnumerable<PaymentDTO>> GetPaymentsByStatusAsync(string status, int bankId); // Added bankId
        Task<IEnumerable<PaymentDTO>> GetAllPaymentsByBankIdAsync(int bankId);
        Task<PaymentDTO> GetPaymentByIdAsync(int paymentId, int bankId);
        Task<PaymentDTO> ApprovePaymentAsync(int paymentId, int bankUserId, int bankId, string? notes);
        Task<PaymentDTO> RejectPaymentAsync(int paymentId, int bankUserId, int bankId, string? notes);
    }

   
}