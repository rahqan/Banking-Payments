//using Banking_Payments.Models.DTO;
//using Banking_Payments.Models;
//using dummy_api.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;
//using Banking_Payments.Services;
//using Banking_Payments.Models.Enums;

//namespace Banking_Payments.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize(Roles = nameof(Role.BankUser))]
//    public class BankUserController : ControllerBase
//    {
//        private readonly BankUserService _bankUserService;
//        private readonly ILogger<BankUserController> _logger;
//        //private readonly IPaymentService _paymentService;


//        public BankUserController(
//            BankUserService bankUserService,
//            ILogger<BankUserController> logger
//            //,IPaymentService paymentService
//            )
//        {
//            _bankUserService = bankUserService;
//            _logger = logger;
//            //_paymentService = paymentService;
//        }

//        // CLIENT ENDPOINTS

//        [HttpPost("clients")]
//        public async Task<ActionResult<ClientDTO>> CreateClientAsync([FromBody] ClientCreationDTO clientCreationDTO)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest();
//            }

//            ClientDTO createdClient = await _bankUserService.CreateClientAsync(clientCreationDTO);

//            if (createdClient!=null)
//            {
//                _logger.LogInformation("Client created successfully: {ClientName}", createdClient.ClientName);
//                return Ok(createdClient);
//            }

//            _logger.LogWarning("Client creation failed: {Message}", createdClient);
//            return BadRequest(createdClient);
//        }

//        [HttpGet("clients")]
//        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAllClients(int bankId)
//        {
//            // get bankId from claims
//            //var bankIdClaim = BankUser.FindFirst("BankId")?.Value;

//            //var bankId=-1;

//            //if (string.IsNullOrEmpty(bankIdClaim) || !long.TryParse(bankIdClaim, out  bankId))
//            //{
//            //    return Unauthorized(new IActionResult<IEnumerable<ClientDTO>>
//            //    {
//            //        Success = false,
//            //        Message = "BankId claim missing or invalid."
//            //    });
//            //}


//            var result = await _bankUserService.GetAllClientsAsync(bankId);

//            if (result!=null)
//                return Ok(result);

//            return StatusCode(500, result);
//        }


//        [HttpGet("clients/{clientId}")]
//        public async Task<ActionResult<ClientDTO>> GetClientById(int clientId)
//        {
//            if (clientId <= 0)
//            {
//                // IActionResult<ClientDTO>.ErrorResult("Invalid client ID")
//                return BadRequest();
//            }

//            var result = await _bankUserService.GetClientByIdAsync(clientId);

//            if (result!=null)
//            {
//                return Ok(result);
//            }
//            else
//            {
//                return NotFound(result);
//            }
//        }

//        [HttpPut("clients/{clientId}")]
//        public async Task<ActionResult<ClientDTO>> UpdateClient(int clientId, [FromBody] ClientDTO clientDTO)
//        {
//            if (!ModelState.IsValid)
//            {
//                //IActionResult<ClientDTO>.ErrorResult("Invalid input data")
//                return BadRequest();
//            }

//            if (clientId != clientDTO.ClientId)
//            {
//                // IActionResult<ClientDTO>.ErrorResult("Client ID mismatch")
//                return BadRequest();
//            }

//            var result = await _bankUserService.UpdateClientAsync(clientDTO);

//            if (result!=null)
//            {
//                _logger.LogInformation("Client updated successfully: {ClientId}", clientId);
//                return Ok(result);
//            }
//            else
//            {
//                _logger.LogWarning("Client update failed");
//                return BadRequest();
//            }
//        }

//        [HttpDelete("clients/{clientId}")]
//        public async Task<ActionResult<bool>> DeleteClient(int clientId)
//        {
//            if (clientId <= 0)
//            {
//                //IActionResult<bool>.ErrorResult("Invalid client ID")
//                return BadRequest();
//            }

//            var result = await _bankUserService.DeleteClientAsync(clientId);

//            if (result!=null)
//            {
//                _logger.LogInformation("Client deleted successfully: {ClientId}", clientId);
//                return Ok(result);
//            }
//            else
//            {
//                _logger.LogWarning("Client deletion failed");
//                return BadRequest();
//            }
//        }

//        // CLIENT VERIFICATION ENDPOINTS

//        [HttpPut("clients/{clientId}/verify")]
//        public async Task<ActionResult<ClientDTO>> VerifyClient(int clientId)
//        {
//            if (!ModelState.IsValid)
//            {
//                //IActionResult<ClientDTO>.ErrorResult("Invalid input data")
//                return BadRequest();
//            }


//            var currentUserId = long.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
//            var currentUserBankId = long.Parse(User.FindFirst("BankId")?.Value ?? "0");

//            var result = await _bankUserService.VerifyClientAsync(clientId, currentUserId, currentUserBankId, request.VerificationStatus, request.Notes);

//            if (result.Success)
//            {
//                _logger.LogInformation("Client verification updated: {ClientId}, Status: {Status}", clientId, request.VerificationStatus);
//                return Ok(result);
//            }
//            else
//            {
//                _logger.LogWarning("Client verification failed: {Message}", result.Message);
//                return BadRequest(result);
//            }
//        }

//        [HttpGet("clients/verification-status/{status}")]
//        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClientsByVerificationStatus(string status)
//        {
//            if (string.IsNullOrEmpty(status) || !Enum.TryParse<VerificationStatus>(status, true, out var parsedStatus))
//            {
//                //IActionResult<IEnumerable<ClientDTO>>.ErrorResult("Invalid verification status")
//                return BadRequest();
//            }

//            var result = await _bankUserService.GetClientsByVerificationStatusAsync(status);

//            if (result!=null)
//            {
//                return Ok(result);
//            }
//            else
//            {
//                return StatusCode(500, result);
//            }
//        }







































//// CLIENT DOCUMENT ENDPOINTS

//[HttpPost("clients/{clientId}/documents")]
//public async Task<ActionResult<BaseResponseDTO<DocumentDTO>>> UploadClientDocument(long clientId, [FromForm] UploadDocumentRequestDTO request)
//{
//    if (clientId <= 0)
//    {
//        return BadRequest(BaseResponseDTO<DocumentDTO>.ErrorResult("Invalid client ID"));
//    }

//    if (request.File == null || request.File.Length == 0)
//    {
//        return BadRequest(BaseResponseDTO<DocumentDTO>.ErrorResult("File is required"));
//    }

//    if (string.IsNullOrEmpty(request.DocType))
//    {
//        return BadRequest(BaseResponseDTO<DocumentDTO>.ErrorResult("Document type is required"));
//    }

//    // Get current user ID and bank ID from claims
//    var currentUserId = long.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
//    var currentUserBankId = long.Parse(User.FindFirst("BankId")?.Value ?? "0");

//    var result = await _bankUserService.UploadClientDocumentAsync(clientId, request.File, currentUserId, currentUserBankId, request.DocType);

//    if (result.Success)
//    {
//        _logger.LogInformation("Document uploaded successfully for client ID: {ClientId}", clientId);
//        return Ok(result);
//    }
//    else
//    {
//        _logger.LogWarning("Document upload failed: {Message}", result.Message);
//        return BadRequest(result);
//    }
//}

//[HttpGet("clients/{clientId}/documents")]
//public async Task<ActionResult<BaseResponseDTO<IEnumerable<DocumentDTO>>>> GetClientDocuments(long clientId)
//{
//    if (clientId <= 0)
//    {
//        return BadRequest(BaseResponseDTO<IEnumerable<DocumentDTO>>.ErrorResult("Invalid client ID"));
//    }

//    var result = await _bankUserService.GetClientDocumentsAsync(clientId);

//    if (result.Success)
//    {
//        return Ok(result);
//    }
//    else
//    {
//        return NotFound(result);
//    }
//}

//// CLIENT USER ENDPOINTS

//[HttpPost("clients/{clientId}/users")]
//public async Task<ActionResult<BaseResponseDTO<ClientUserCreationDTO>>> CreateClientUser(long clientId, [FromBody] RegisterDTO userDTO)
//{
//    if (!ModelState.IsValid)
//    {
//        return BadRequest(BaseResponseDTO<ClientUserCreationDTO>.ErrorResult("Invalid input data"));
//    }

//    userDTO.ClientId = clientId;
//    var result = await _bankUserService.CreateClientUserAsync(userDTO);

//    if (result.Success)
//    {
//        _logger.LogInformation("Client user created successfully for client ID: {ClientId}", clientId);
//        return Ok(result);
//    }
//    else
//    {
//        _logger.LogWarning("Client user creation failed: {Message}", result.Message);
//        return BadRequest(result);
//    }
//}

//[HttpGet("clients/{clientId}/users")]
//public async Task<ActionResult<BaseResponseDTO<IEnumerable<UserDTO>>>> GetClientUsersByClientId(long clientId)
//{
//    if (clientId <= 0)
//    {
//        return BadRequest(BaseResponseDTO<IEnumerable<UserDTO>>.ErrorResult("Invalid client ID"));
//    }

//    var result = await _bankUserService.GetAllClientUsersByClientIdAsync(clientId);

//    if (result.Success)
//    {
//        return Ok(result);
//    }
//    else
//    {
//        return NotFound(result);
//    }
//}

//[HttpGet("clients/{clientId}/users/{userId}")]
//public async Task<ActionResult<BaseResponseDTO<UserDTO>>> GetClientUserById(long clientId, long userId)
//{
//    if (userId <= 0)
//    {
//        return BadRequest(BaseResponseDTO<UserDTO>.ErrorResult("Invalid user ID"));
//    }

//    var result = await _bankUserService.GetClienUserByIdAsync(userId);

//    if (result.Success)
//    {
//        return Ok(result);
//    }
//    else
//    {
//        return NotFound(result);
//    }
//}

//[HttpDelete("clients/{clientId}/users/{userId}")]
//public async Task<ActionResult<BaseResponseDTO<bool>>> DeleteClientUser(long clientId, long userId)
//{
//    if (userId <= 0)
//    {
//        return BadRequest(BaseResponseDTO<bool>.ErrorResult("Invalid user ID"));
//    }

//    var result = await _bankUserService.DeleteClientUserAsync(userId);

//    if (result.Success)
//    {
//        _logger.LogInformation("Client user deleted successfully: {UserId}", userId);
//        return Ok(result);
//    }
//    else
//    {
//        _logger.LogWarning("Client user deletion failed: {Message}", result.Message);
//        return BadRequest(result);
//    }
//}



//// PAYMENT APPROVAL 
//[HttpGet("payments/pending")]
//public async Task<ActionResult<BaseResponseDTO<IEnumerable<PaymentDTO>>>> GetPendingPayments()
//{
//    var result = await _paymentService.GetPendingPaymentsAsync();

//    if (result.Success)
//    {
//        return Ok(result);
//    }
//    else
//    {
//        return StatusCode(500, result);
//    }
//}

//[HttpGet("payments")]
//public async Task<ActionResult<BaseResponseDTO<IEnumerable<PaymentDTO>>>> GetPaymentsByStatus([FromQuery] string status)
//{
//    if (string.IsNullOrEmpty(status))
//    {
//        return BadRequest(BaseResponseDTO<IEnumerable<PaymentDTO>>.ErrorResult("Status parameter is required"));
//    }

//    var result = await _paymentService.GetPaymentsByStatusAsync(status);

//    if (result.Success)
//    {
//        return Ok(result);
//    }
//    else
//    {
//        return BadRequest(result);
//    }
//}
//[HttpPost("payments/all")]
//public async Task<ActionResult<BaseResponseDTO<IEnumerable<PaymentDTO>>>> GetAllPaymentsByBankUser(long BankId)
//{
//    if (BankId < 0)
//    {
//        return BadRequest(BaseResponseDTO<IEnumerable<PaymentDTO>>.ErrorResult("Status parameter is required"));
//    }

//    var result = await _paymentService.GetAllPaymentsByBankUserId(BankId);

//    if (result.Success)
//    {
//        return Ok(result);
//    }
//    else
//    {
//        return BadRequest(result);
//    }
//}

//[HttpPut("payments/{paymentId}/approve")]
//public async Task<ActionResult<BaseResponseDTO<PaymentDTO>>> ApprovePayment(long paymentId, [FromBody] ApprovePaymentRequestDTO request)
//{
//    if (paymentId <= 0)
//    {
//        return BadRequest(BaseResponseDTO<PaymentDTO>.ErrorResult("Invalid payment ID"));
//    }

//    var currentBankUserId = long.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

//    var result = await _paymentService.ApprovePaymentAsync(paymentId, currentBankUserId, request.Notes);

//    if (result.Success)
//    {
//        _logger.LogInformation("Payment approved successfully: {PaymentId}, ApprovedBy: {BankUserId}", paymentId, currentBankUserId);
//        return Ok(result);
//    }
//    else
//    {
//        _logger.LogWarning("Payment approval failed: {Message}", result.Message);
//        return BadRequest(result);
//    }
//}

//[HttpPut("payments/{paymentId}/reject")]
//public async Task<ActionResult<BaseResponseDTO<PaymentDTO>>> RejectPayment(long paymentId, [FromBody] ApprovePaymentRequestDTO request)
//{
//    if (paymentId <= 0)
//    {
//        return BadRequest(BaseResponseDTO<PaymentDTO>.ErrorResult("Invalid payment ID"));
//    }

//    var currentBankUserId = long.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

//    var result = await _paymentService.RejectPaymentAsync(paymentId, currentBankUserId, request.Notes);

//    if (result.Success)
//    {
//        _logger.LogInformation("Payment rejected: {PaymentId}, RejectedBy: {BankUserId}", paymentId, currentBankUserId);
//        return Ok(result);
//    }
//    else
//    {
//        _logger.LogWarning("Payment rejection failed: {Message}", result.Message);
//        return BadRequest(result);
//    }
//}

//[HttpGet("payments/{paymentId}")]
//public async Task<ActionResult<BaseResponseDTO<PaymentDTO>>> GetPaymentById(long paymentId)
//{
//    if (paymentId <= 0)
//    {
//        return BadRequest(BaseResponseDTO<PaymentDTO>.ErrorResult("Invalid payment ID"));
//    }

//    var result = await _paymentService.GetPaymentByIdAsync(paymentId);

//    if (result.Success)
//    {
//        return Ok(result);
//    }
//    else
//    {
//        return NotFound(result);
//    }
//}



//    }
//}


using Banking_Payments.Models.DTO;
using Banking_Payments.Models;
using Banking_Payments.Services;
using Banking_Payments.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banking_Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(Role.BankUser))]
    public class BankUserController : ControllerBase
    {
        private readonly IBankUserService _bankUserService;
        private readonly ILogger<BankUserController> _logger;

        public BankUserController(
            IBankUserService bankUserService,
            ILogger<BankUserController> logger)
        {
            _bankUserService = bankUserService;
            _logger = logger;
        }

        // CLIENT ENDPOINTS

        [HttpPost("clients")]
        public async Task<ActionResult<ClientDTO>> CreateClientAsync([FromBody] ClientCreationDTO clientCreationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdClient = await _bankUserService.CreateClientAsync(clientCreationDTO);
                _logger.LogInformation("Client created successfully: {ClientName}", createdClient.ClientName);
                return Ok(createdClient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Client creation failed");
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("clients")]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAllClients([FromQuery] int bankId)
        {
            // TODO: Get bankId from claims instead of query parameter for better security
            // var bankIdClaim = User.FindFirst("BankId")?.Value;
            // if (string.IsNullOrEmpty(bankIdClaim) || !int.TryParse(bankIdClaim, out int bankId))
            // {
            //     return Unauthorized(new { message = "BankId claim missing or invalid." });
            // }

            try
            {
                var result = await _bankUserService.GetAllClientsAsync(bankId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve clients for bank {BankId}", bankId);
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
                var result = await _bankUserService.GetClientByIdAsync(clientId);

                if (result == null)
                {
                    return NotFound(new { message = $"Client with ID {clientId} not found" });
                }

                return Ok(result);
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
                var result = await _bankUserService.UpdateClientAsync(clientId, clientDTO);
                _logger.LogInformation("Client updated successfully: {ClientId}", clientId);
                return Ok(result);
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
                var result = await _bankUserService.DeleteClientAsync(clientId);

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

        // CLIENT VERIFICATION ENDPOINTS

        [HttpPut("clients/{clientId}/verify")]
        public async Task<ActionResult<ClientDTO>> VerifyClient(int clientId, [FromBody] ClientVerificationRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var currentUserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
                var currentUserBankId = int.Parse(User.FindFirst("BankId")?.Value ?? "0");

                if (currentUserId == 0 || currentUserBankId == 0)
                {
                    return Unauthorized(new { message = "User authentication information missing" });
                }

                var result = await _bankUserService.VerifyClientAsync(
                    clientId,
                    currentUserId,
                    currentUserBankId,
                    request.VerificationStatus,
                    request.Notes);

                _logger.LogInformation("Client verification updated: {ClientId}, Status: {Status}",
                    clientId, request.VerificationStatus);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Client not found for verification: {ClientId}", clientId);
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized verification attempt for client: {ClientId}", clientId);
                return Forbid();
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
                var result = await _bankUserService.GetClientsByVerificationStatusAsync(status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve clients by verification status: {Status}", status);
                return StatusCode(500, new { message = "An error occurred while retrieving clients." });
            }
        }
    }
}