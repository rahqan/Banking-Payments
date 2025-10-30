//using Banking_Payments.DTOs;
//using Banking_Payments.Models;
//using Banking_Payments.Models.Enums;
//using Banking_Payments.Repositories;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;

//namespace Banking_Payments.Services
//{
//    public class PaymentService : IPaymentService
//    {
//        private readonly IPaymentRepository _paymentRepo;
//        private readonly ILogger<PaymentService> _logger;

//        public PaymentService(IPaymentRepository paymentRepo, ILogger<PaymentService> logger)
//        {
//            _paymentRepo = paymentRepo;
//            _logger = logger;
//        }

//        private static PaymentDTO MapToDto(Payment p)
//        {
//            return new PaymentDTO
//            {
//                PaymentId = p.PaymentId,
//                Amount = p.Amount,
//                PaymentDate = p.PaymentDate,
//                Status = p.status,
//                Type = p.Type,
//                BeneficiaryId = p.BeneficiaryId,
//                BeneficiaryName = p.Beneficiary?.Name,
//                ClientId = p.ClientId,
//                ClientName = p.Client?.ClientName,
//                ApprovedById = p.BankUserId,
//                ApprovedByName = p.ApprovedBy?.Name
//            };
//        }

//        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPendingPaymentsAsync()
//        {
//            var payments = await _paymentRepo.GetPendingPaymentsAsync();
//            return new OkObjectResult(payments.Select(MapToDto));
//        }

//        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsByStatusAsync(string status)
//        {
//            if (!Enum.TryParse(status, true, out VerificationStatus parsedStatus))
//                return new BadRequestObjectResult("Invalid status value.");

//            var payments = await _paymentRepo.GetPaymentsByStatusAsync(parsedStatus);
//            return new OkObjectResult(payments.Select(MapToDto));
//        }

//        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetAllPaymentsByBankUserId(long bankId)
//        {
//            if (bankId <= 0)
//                return new BadRequestObjectResult("Invalid Bank ID");

//            var payments = await _paymentRepo.GetAllPaymentsByBankUserIdAsync(bankId);
//            return new OkObjectResult(payments.Select(MapToDto));
//        }

//        public async Task<ActionResult<PaymentDTO>> GetPaymentByIdAsync(long paymentId)
//        {
//            if (paymentId <= 0)
//                return new BadRequestObjectResult("Invalid payment ID");

//            var payment = await _paymentRepo.GetPaymentByIdAsync(paymentId);
//            if (payment == null)
//                return new NotFoundObjectResult("Payment not found");

//            return new OkObjectResult(MapToDto(payment));
//        }

//        public async Task<ActionResult<PaymentDTO>> ApprovePaymentAsync(long paymentId, long bankUserId, string? notes)
//        {
//            if (paymentId <= 0 || bankUserId <= 0)
//                return new BadRequestObjectResult("Invalid payment or bank user ID");

//            var payment = await _paymentRepo.GetPaymentByIdAsync(paymentId);
//            if (payment == null)
//                return new NotFoundObjectResult("Payment not found");

//            if (payment.status == VerificationStatus.Verified)
//                return new BadRequestObjectResult("Payment is already approved");

//            payment.status = VerificationStatus.Verified;
//            payment.BankUserId = (int)bankUserId;

//            if (!string.IsNullOrWhiteSpace(notes))
//                _logger.LogInformation("Approval notes for payment {PaymentId}: {Notes}", paymentId, notes);

//            await _paymentRepo.UpdatePaymentAsync(payment);

//            _logger.LogInformation("Payment approved successfully: {PaymentId}, ApprovedBy: {BankUserId}", paymentId, bankUserId);
//            return new OkObjectResult(MapToDto(payment));
//        }

//        public async Task<ActionResult<PaymentDTO>> RejectPaymentAsync(long paymentId, long bankUserId, string? notes)
//        {
//            if (paymentId <= 0 || bankUserId <= 0)
//                return new BadRequestObjectResult("Invalid payment or bank user ID");

//            var payment = await _paymentRepo.GetPaymentByIdAsync(paymentId);
//            if (payment == null)
//                return new NotFoundObjectResult("Payment not found");

//            if (payment.status == VerificationStatus.Rejected)
//                return new BadRequestObjectResult("Payment already rejected");

//            payment.status = VerificationStatus.Rejected;
//            payment.BankUserId = (int)bankUserId;

//            if (!string.IsNullOrWhiteSpace(notes))
//                _logger.LogInformation("Rejection notes for payment {PaymentId}: {Notes}", paymentId, notes);

//            await _paymentRepo.UpdatePaymentAsync(payment);

//            _logger.LogInformation("Payment rejected successfully: {PaymentId}, RejectedBy: {BankUserId}", paymentId, bankUserId);
//            return new OkObjectResult(MapToDto(payment));
//        }
//    }
//}

using Banking_Payments.DTOs;
using Banking_Payments.Models;
using Banking_Payments.Models.Enums;
using Banking_Payments.Repositories;
using Microsoft.Extensions.Logging;

namespace Banking_Payments.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IPaymentRepository paymentRepo, ILogger<PaymentService> logger)
        {
            _paymentRepo = paymentRepo;
            _logger = logger;
        }

        private static PaymentDTO MapToDto(Payment p)
        {
            return new PaymentDTO
            {
                PaymentId = p.PaymentId,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                Status = p.status,
                Type = p.Type,
                BeneficiaryId = p.BeneficiaryId,
                BeneficiaryName = p.Beneficiary?.Name,
                ClientId = p.ClientId,
                ClientName = p.Client?.ClientName,
                ApprovedById = p.BankUserId,
                ApprovedByName = p.ApprovedBy?.Name
            };
        }

        // CRITICAL: Now filters by bankId - only returns pending payments from THIS bank
        public async Task<IEnumerable<PaymentDTO>> GetPendingPaymentsAsync(int bankId)
        {
            var payments = await _paymentRepo.GetPendingPaymentsByBankIdAsync(bankId);
            return payments.Select(MapToDto);
        }

        // CRITICAL: Now filters by bankId - only returns payments with this status from THIS bank
        public async Task<IEnumerable<PaymentDTO>> GetPaymentsByStatusAsync(string status, int bankId)
        {
            if (!Enum.TryParse(status, true, out VerificationStatus parsedStatus))
                throw new ArgumentException("Invalid status value.");

            var payments = await _paymentRepo.GetPaymentsByStatusAndBankIdAsync(parsedStatus, bankId);
            return payments.Select(MapToDto);
        }

        public async Task<IEnumerable<PaymentDTO>> GetAllPaymentsByBankIdAsync(int bankId)
        {
            if (bankId <= 0)
                throw new ArgumentException("Invalid Bank ID");

            var payments = await _paymentRepo.GetAllPaymentsByBankIdAsync(bankId);
            return payments.Select(MapToDto);
        }

        public async Task<PaymentDTO> GetPaymentByIdAsync(int paymentId, int bankId)
        {
            if (paymentId <= 0)
                throw new ArgumentException("Invalid payment ID");

            var payment = await _paymentRepo.GetPaymentByIdAsync(paymentId);

            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {paymentId} not found");

            // Authorization check: Payment's client must belong to the same bank
            if (payment.Client?.BankId != bankId)
                throw new UnauthorizedAccessException("You don't have access to this payment");

            return MapToDto(payment);
        }

        public async Task<PaymentDTO> ApprovePaymentAsync(int paymentId, int bankUserId, int bankId, string? notes)
        {
            if (paymentId <= 0 || bankUserId <= 0)
                throw new ArgumentException("Invalid payment or bank user ID");

            var payment = await _paymentRepo.GetPaymentByIdAsync(paymentId);

            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {paymentId} not found");

            // Authorization check: Payment's client must belong to the same bank as the bank user
            if (payment.Client?.BankId != bankId)
                throw new UnauthorizedAccessException("You can only approve payments from your own bank");

            // CRITICAL: Check current status
            if (payment.status == VerificationStatus.Verified)
                throw new InvalidOperationException("Payment is already approved");

            if (payment.status == VerificationStatus.Rejected)
                throw new InvalidOperationException("Cannot approve a rejected payment");

            payment.status = VerificationStatus.Verified;
            payment.BankUserId = bankUserId;

            if (!string.IsNullOrWhiteSpace(notes))
                _logger.LogInformation("Approval notes for payment {PaymentId}: {Notes}", paymentId, notes);

            await _paymentRepo.UpdatePaymentAsync(payment);

            _logger.LogInformation("Payment approved successfully: {PaymentId}, ApprovedBy: {BankUserId}, Bank: {BankId}",
                paymentId, bankUserId, bankId);

            return MapToDto(payment);
        }

        public async Task<PaymentDTO> RejectPaymentAsync(int paymentId, int bankUserId, int bankId, string? notes)
        {
            if (paymentId <= 0 || bankUserId <= 0)
                throw new ArgumentException("Invalid payment or bank user ID");

            var payment = await _paymentRepo.GetPaymentByIdAsync(paymentId);

            if (payment == null)
                throw new KeyNotFoundException($"Payment with ID {paymentId} not found");

            // Authorization check: Payment's client must belong to the same bank as the bank user
            if (payment.Client?.BankId != bankId)
                throw new UnauthorizedAccessException("You can only reject payments from your own bank");

            // CRITICAL: Check current status
            if (payment.status == VerificationStatus.Rejected)
                throw new InvalidOperationException("Payment is already rejected");

            if (payment.status == VerificationStatus.Verified)
                throw new InvalidOperationException("Cannot reject an approved payment");

            payment.status = VerificationStatus.Rejected;
            payment.BankUserId = bankUserId;

            if (!string.IsNullOrWhiteSpace(notes))
                _logger.LogInformation("Rejection notes for payment {PaymentId}: {Notes}", paymentId, notes);

            await _paymentRepo.UpdatePaymentAsync(payment);

            _logger.LogInformation("Payment rejected successfully: {PaymentId}, RejectedBy: {BankUserId}, Bank: {BankId}",
                paymentId, bankUserId, bankId);

            return MapToDto(payment);
        }
    }
}