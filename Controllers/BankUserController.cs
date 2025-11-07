using Banking_Payments.Models;
using Banking_Payments.Models.DTO;
using Banking_Payments.Models.Enums;
using Banking_Payments.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Banking_Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = nameof(Role.BankUser))]
    [Authorize(Roles = nameof(Role.BankUser))]
    public class BankUserController : ControllerBase
    {
        private readonly IBankUserService _bankUserService;
        private readonly ILogger<BankUserController> _logger;
        private readonly IPaymentService _paymentService;

        public BankUserController(
            IBankUserService bankUserService,
            ILogger<BankUserController> logger,
            IPaymentService paymentService)
        {
            _bankUserService = bankUserService;
            _logger = logger;
            _paymentService = paymentService;
        }

        // Helper method to get BankId from claims
        private int GetBankIdFromClaims()
        {
            var bankIdClaim = User.FindFirst("BankId")?.Value;
            if (string.IsNullOrEmpty(bankIdClaim) || !int.TryParse(bankIdClaim, out int bankId))
            {
                throw new UnauthorizedAccessException("BankId claim missing or invalid");
            }
            return bankId;
        }

        // Helper method to get BankUserId from claims
        private int GetBankUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int bankUserId))
            {
                throw new UnauthorizedAccessException("BankUserId claim missing or invalid");
            }
            return bankUserId;
        }

        // ==================== CLIENT ENDPOINTS ====================

        //[HttpPost("clients")]
        //public async Task<ActionResult<ClientDTO>> CreateClientAsync([FromBody] ClientCreationDTO clientCreationDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var bankId = GetBankIdFromClaims();
        //        var bankUserId = GetBankUserIdFromClaims();

        //        // Ensure the client is being created for the bank user's bank
        //        //if (clientCreationDTO.BankId != bankId)
        //        //{
        //        //    return Forbid();
        //        //}

        //        var createdClient = await _bankUserService.CreateClientAsync(clientCreationDTO,bankId,bankUserId);
        //        _logger.LogInformation("Client created successfully: {ClientName}", createdClient.ClientName);
        //        return Ok(createdClient);
        //    }
        //    catch (UnauthorizedAccessException ex)
        //    {
        //        return Unauthorized(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Client creation failed");
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}


        [HttpPost("clients")]
        public async Task<ActionResult<ClientDTO>> CreateClientAsync([FromBody] ClientCreationDTO clientCreationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var bankId = GetBankIdFromClaims();
                var bankUserId = GetBankUserIdFromClaims();

                var createdClient = await _bankUserService.CreateClientAsync(clientCreationDTO, bankId, bankUserId);
                _logger.LogInformation("Client created successfully: {ClientName}", createdClient.ClientName);

                return Ok(new
                {
                    message = "Client created successfully",
                    clientId = createdClient.ClientId,
                    clientName = createdClient.ClientName
                });
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                // Handle duplicate key error (Error Number: 2601 or 2627)
                if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                {
                    _logger.LogWarning($"Duplicate client email: {clientCreationDTO.ClientEmail}");

                    return Conflict(new
                    {
                        message = "A client with this email address already exists. Please use a different email.",
                        error = "DUPLICATE_EMAIL"
                    });
                }

                _logger.LogError(ex, "Database error during client creation");
                return StatusCode(500, new
                {
                    message = "An error occurred while creating the client. Please try again."
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Client creation failed");
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred. Please try again or contact support."
                });
            }
        }

        [HttpGet("clients")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAllClients()
        {
            try
            {
                var bankId = GetBankIdFromClaims();
                var result = await _bankUserService.GetAllClientsAsync(bankId);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve clients");
                return StatusCode(500, new { message = "An error occurred while retrieving clients." });
            }
        }

        [HttpGet("clients/{clientId}")]
        public async Task<ActionResult<ClientDTO>> GetClientById(int clientId)
        {
            if (clientId <= 0)
            {
                return BadRequest(new { message = "Invalid client ID" });
            }

            try
            {
                var bankId = GetBankIdFromClaims();
                var result = await _bankUserService.GetClientByIdAsync(clientId, bankId);

                if (result == null)
                {
                    return NotFound(new { message = $"Client with ID {clientId} not found" });
                }

                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt for client {ClientId}", clientId);
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve client {ClientId}", clientId);
                return StatusCode(500, new { message = "An error occurred while retrieving the client." });
            }
        }

        [HttpPut("clients/{clientId}")]
        public async Task<ActionResult<ClientDTO>> UpdateClient(int clientId, [FromBody] ClientDTO clientDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (clientId != clientDTO.ClientId)
            {
                return BadRequest(new { message = "Client ID mismatch" });
            }

            try
            {
                var bankId = GetBankIdFromClaims();
                var result = await _bankUserService.UpdateClientAsync(clientId, clientDTO, bankId);
                _logger.LogInformation("Client updated successfully: {ClientId}", clientId);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized update attempt for client {ClientId}", clientId);
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Client not found for update: {ClientId}", clientId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Client update failed for {ClientId}", clientId);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("clients/{clientId}")]
        public async Task<ActionResult<bool>> DeleteClient(int clientId)
        {
            if (clientId <= 0)
            {
                return BadRequest(new { message = "Invalid client ID" });
            }

            try
            {
                var bankId = GetBankIdFromClaims();
                var result = await _bankUserService.DeleteClientAsync(clientId, bankId);

                if (result)
                {
                    _logger.LogInformation("Client deleted successfully: {ClientId}", clientId);
                    return Ok(new { success = true, message = "Client deleted successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Failed to delete client" });
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized delete attempt for client {ClientId}", clientId);
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Client not found for deletion: {ClientId}", clientId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Client deletion failed for {ClientId}", clientId);
                return BadRequest(new { message = ex.Message });
            }
        }

        // ==================== CLIENT VERIFICATION ENDPOINTS ====================

        [HttpPut("clients/{clientId}/verify")]
        public async Task<ActionResult<ClientDTO>> VerifyClient(int clientId, [FromBody] ClientVerificationRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var currentUserId = GetBankUserIdFromClaims();
                var bankId = GetBankIdFromClaims();

                var result = await _bankUserService.VerifyClientAsync(
                    clientId,
                    currentUserId,
                    bankId,
                    Convert.ToString(request.VerificationStatus),
                    request.Notes);

                _logger.LogInformation("Client verification updated: {ClientId}, Status: {Status}", clientId, request.VerificationStatus);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized verification attempt for client: {ClientId}", clientId);
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Client not found for verification: {ClientId}", clientId);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Client verification failed for {ClientId}", clientId);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("clients/verification-status/{status}")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClientsByVerificationStatus(string status)
        {
            if (string.IsNullOrEmpty(status) || !Enum.TryParse<VerificationStatus>(status, true, out var parsedStatus))
            {
                return BadRequest(new { message = "Invalid verification status" });
            }

            try
            {
                var bankId = GetBankIdFromClaims();
                var result = await _bankUserService.GetClientsByVerificationStatusAsync(status, bankId);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve clients by verification status: {Status}", status);
                return StatusCode(500, new { message = "An error occurred while retrieving clients." });
            }
        }

        // ==================== PAYMENT ENDPOINTS ====================

        [HttpGet("payments/pending")]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPendingPayments()
        {
            try
            {
                var bankId = GetBankIdFromClaims(); // CRITICAL: Get bank from claims
                var result = await _paymentService.GetPendingPaymentsAsync(bankId); // CRITICAL: Pass bankId
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve pending payments");
                return StatusCode(500, new { message = "An error occurred while retrieving pending payments." });
            }
        }

        [HttpGet("payments/status/{status}")]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest(new { message = "Invalid payment status" });
            }

            try
            {
                var bankId = GetBankIdFromClaims(); // CRITICAL: Get bank from claims
                var result = await _paymentService.GetPaymentsByStatusAsync(status, bankId); // CRITICAL: Pass bankId
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid status provided: {Status}", status);
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve payments by status: {Status}", status);
                return StatusCode(500, new { message = "An error occurred while retrieving payments." });
            }
        }

        [HttpGet("payments/bank")]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsByBank()
        {
            try
            {
                var bankId = GetBankIdFromClaims();
                var result = await _paymentService.GetAllPaymentsByBankIdAsync(bankId);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt for bank payments");
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve payments for bank");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("payments/{paymentId}")]
        public async Task<ActionResult<PaymentDTO>> GetPaymentById(int paymentId)
        {
            if (paymentId <= 0)
            {
                return BadRequest(new { message = "Invalid payment ID" });
            }

            try
            {
                var bankId = GetBankIdFromClaims();
                var result = await _paymentService.GetPaymentByIdAsync(paymentId, bankId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Payment not found: {PaymentId}", paymentId);
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt for payment: {PaymentId}", paymentId);
                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve payment {PaymentId}", paymentId);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("payments/{paymentId}/approve")]
        public async Task<ActionResult<PaymentDTO>> ApprovePayment(int paymentId, [FromBody] ApprovePaymentRequestDTO request)
        {
            if (paymentId <= 0)
            {
                return BadRequest(new { message = "Invalid payment ID" });
            }

            if (request == null)
            {
                return BadRequest(new { message = "Approval request body is missing" });
            }

            try
            {
                var bankUserId = GetBankUserIdFromClaims();
                var bankId = GetBankIdFromClaims();

                var result = await _paymentService.ApprovePaymentAsync(paymentId, bankUserId, bankId, request.Notes);
                _logger.LogInformation("Payment approved: {PaymentId} by BankUser {BankUserId} from Bank {BankId}",
                    paymentId, bankUserId, bankId);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized approval attempt: PaymentId={PaymentId}", paymentId);
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Payment not found for approval: {PaymentId}", paymentId);
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation for payment approval: {PaymentId}", paymentId);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to approve payment {PaymentId}", paymentId);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("payments/{paymentId}/reject")]
        public async Task<ActionResult<PaymentDTO>> RejectPayment(int paymentId, [FromBody] ApprovePaymentRequestDTO request)
        {
            if (paymentId <= 0)
            {
                return BadRequest(new { message = "Invalid payment ID" });
            }

            if (request == null)
            {
                return BadRequest(new { message = "Rejection request body is missing" });
            }

            try
            {
                var bankUserId = GetBankUserIdFromClaims();
                var bankId = GetBankIdFromClaims();

                var result = await _paymentService.RejectPaymentAsync(paymentId, bankUserId, bankId, request.Notes);
                _logger.LogInformation("Payment rejected: {PaymentId} by BankUser {BankUserId} from Bank {BankId}",
                    paymentId, bankUserId, bankId);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized rejection attempt: PaymentId={PaymentId}", paymentId);
                return Forbid();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Payment not found for rejection: {PaymentId}", paymentId);
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation for payment rejection: {PaymentId}", paymentId);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reject payment {PaymentId}", paymentId);
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
