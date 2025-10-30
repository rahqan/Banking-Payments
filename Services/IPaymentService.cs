//using Banking_Payments.DTOs;
//using Microsoft.AspNetCore.Mvc;

//namespace Banking_Payments.Services
//{
//    public interface IPaymentService
//    {
//        Task<ActionResult<IEnumerable<PaymentDTO>>> GetPendingPaymentsAsync();
//        Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsByStatusAsync(string status);
//        Task<ActionResult<IEnumerable<PaymentDTO>>> GetAllPaymentsByBankUserId(long bankId);
//        Task<ActionResult<PaymentDTO>> GetPaymentByIdAsync(long paymentId);
//        Task<ActionResult<PaymentDTO>> ApprovePaymentAsync(long paymentId, long bankUserId, string? notes);
//        Task<ActionResult<PaymentDTO>> RejectPaymentAsync(long paymentId, long bankUserId, string? notes);
//    }
//}

using Banking_Payments.DTOs;
using Banking_Payments.Models;
using Banking_Payments.Models.Enums;

namespace Banking_Payments.Services
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