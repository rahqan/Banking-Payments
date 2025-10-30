////using Banking_Payments.Models.DTO;
////using Banking_Payments.Models;
////using dummy_api.Services;
////using Microsoft.AspNetCore.Authorization;
////using Microsoft.AspNetCore.Http.HttpResults;
////using Microsoft.AspNetCore.Mvc;
////using Banking_Payments.Services;
////using Banking_Payments.Models.Enums;

////namespace Banking_Payments.Controllers
////{
////    [Route("api/[controller]")]
////    [ApiController]
////    [Authorize(Roles = nameof(Role.BankUser))]
////    public class BankUserController : ControllerBase
////    {
////        private readonly BankUserService _bankUserService;
////        private readonly ILogger<BankUserController> _logger;
////        //private readonly IPaymentService _paymentService;


////        public BankUserController(
////            BankUserService bankUserService,
////            ILogger<BankUserController> logger
////            //,IPaymentService paymentService
////            )
////        {
////            _bankUserService = bankUserService;
////            _logger = logger;
////            //_paymentService = paymentService;
////        }

////        // CLIENT ENDPOINTS

////        [HttpPost("clients")]
////        public async Task<ActionResult<ClientDTO>> CreateClientAsync([FromBody] ClientCreationDTO clientCreationDTO)
////        {
////            if (!ModelState.IsValid)
////            {
////                return BadRequest();
////            }

////            ClientDTO createdClient = await _bankUserService.CreateClientAsync(clientCreationDTO);

////            if (createdClient!=null)
////            {
////                _logger.LogInformation("Client created successfully: {ClientName}", createdClient.ClientName);
////                return Ok(createdClient);
////            }

////            _logger.LogWarning("Client creation failed: {Message}", createdClient);
////            return BadRequest(createdClient);
////        }

////        [HttpGet("clients")]
////        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAllClients(int bankId)
////        {
////            // get bankId from claims
////            //var bankIdClaim = BankUser.FindFirst("BankId")?.Value;

////            //var bankId=-1;

////            //if (string.IsNullOrEmpty(bankIdClaim) || !long.TryParse(bankIdClaim, out  bankId))
////            //{
////            //    return Unauthorized(new IActionResult<IEnumerable<ClientDTO>>
////            //    {
////            //        Success = false,
////            //        Message = "BankId claim missing or invalid."
////            //    });
////            //}


////            var result = await _bankUserService.GetAllClientsAsync(bankId);

////            if (result!=null)
////                return Ok(result);

////            return StatusCode(500, result);
////        }


////        [HttpGet("clients/{clientId}")]
////        public async Task<ActionResult<ClientDTO>> GetClientById(int clientId)
////        {
////            if (clientId <= 0)
////            {
////                // IActionResult<ClientDTO>.ErrorResult("Invalid client ID")
////                return BadRequest();
////            }

////            var result = await _bankUserService.GetClientByIdAsync(clientId);

////            if (result!=null)
////            {
////                return Ok(result);
////            }
////            else
////            {
////                return NotFound(result);
////            }
////        }

////        [HttpPut("clients/{clientId}")]
////        public async Task<ActionResult<ClientDTO>> UpdateClient(int clientId, [FromBody] ClientDTO clientDTO)
////        {
////            if (!ModelState.IsValid)
////            {
////                //IActionResult<ClientDTO>.ErrorResult("Invalid input data")
////                return BadRequest();
////            }

////            if (clientId != clientDTO.ClientId)
////            {
////                // IActionResult<ClientDTO>.ErrorResult("Client ID mismatch")
////                return BadRequest();
////            }

////            var result = await _bankUserService.UpdateClientAsync(clientDTO);

////            if (result!=null)
////            {
////                _logger.LogInformation("Client updated successfully: {ClientId}", clientId);
////                return Ok(result);
////            }
////            else
////            {
////                _logger.LogWarning("Client update failed");
////                return BadRequest();
////            }
////        }

////        [HttpDelete("clients/{clientId}")]
////        public async Task<ActionResult<bool>> DeleteClient(int clientId)
////        {
////            if (clientId <= 0)
////            {
////                //IActionResult<bool>.ErrorResult("Invalid client ID")
////                return BadRequest();
////            }

////            var result = await _bankUserService.DeleteClientAsync(clientId);

////            if (result!=null)
////            {
////                _logger.LogInformation("Client deleted successfully: {ClientId}", clientId);
////                return Ok(result);
////            }
////            else
////            {
////                _logger.LogWarning("Client deletion failed");
////                return BadRequest();
////            }
////        }

////        // CLIENT VERIFICATION ENDPOINTS

////        [HttpPut("clients/{clientId}/verify")]
////        public async Task<ActionResult<ClientDTO>> VerifyClient(int clientId)
////        {
////            if (!ModelState.IsValid)
////            {
////                //IActionResult<ClientDTO>.ErrorResult("Invalid input data")
////                return BadRequest();
////            }


////            var currentUserId = long.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
////            var currentUserBankId = long.Parse(User.FindFirst("BankId")?.Value ?? "0");

////            var result = await _bankUserService.VerifyClientAsync(clientId, currentUserId, currentUserBankId, request.VerificationStatus, request.Notes);

////            if (result.Success)
////            {
////                _logger.LogInformation("Client verification updated: {ClientId}, Status: {Status}", clientId, request.VerificationStatus);
////                return Ok(result);
////            }
////            else
////            {
////                _logger.LogWarning("Client verification failed: {Message}", result.Message);
////                return BadRequest(result);
////            }
////        }

////        [HttpGet("clients/verification-status/{status}")]
////        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClientsByVerificationStatus(string status)
////        {
////            if (string.IsNullOrEmpty(status) || !Enum.TryParse<VerificationStatus>(status, true, out var parsedStatus))
////            {
////                //IActionResult<IEnumerable<ClientDTO>>.ErrorResult("Invalid verification status")
////                return BadRequest();
////            }

////            var result = await _bankUserService.GetClientsByVerificationStatusAsync(status);

////            if (result!=null)
////            {
////                return Ok(result);
////            }
////            else
////            {
////                return StatusCode(500, result);
////            }
////        }







































////// CLIENT DOCUMENT ENDPOINTS

////[HttpPost("clients/{clientId}/documents")]
////public async Task<ActionResult<BaseResponseDTO<DocumentDTO>>> UploadClientDocument(long clientId, [FromForm] UploadDocumentRequestDTO request)
////{
////    if (clientId <= 0)
////    {
////        return BadRequest(BaseResponseDTO<DocumentDTO>.ErrorResult("Invalid client ID"));
////    }

////    if (request.File == null || request.File.Length == 0)
////    {
////        return BadRequest(BaseResponseDTO<DocumentDTO>.ErrorResult("File is required"));
////    }

////    if (string.IsNullOrEmpty(request.DocType))
////    {
////        return BadRequest(BaseResponseDTO<DocumentDTO>.ErrorResult("Document type is required"));
////    }

////    // Get current user ID and bank ID from claims
////    var currentUserId = long.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
////    var currentUserBankId = long.Parse(User.FindFirst("BankId")?.Value ?? "0");

////    var result = await _bankUserService.UploadClientDocumentAsync(clientId, request.File, currentUserId, currentUserBankId, request.DocType);

////    if (result.Success)
////    {
////        _logger.LogInformation("Document uploaded successfully for client ID: {ClientId}", clientId);
////        return Ok(result);
////    }
////    else
////    {
////        _logger.LogWarning("Document upload failed: {Message}", result.Message);
////        return BadRequest(result);
////    }
////}

////[HttpGet("clients/{clientId}/documents")]
////public async Task<ActionResult<BaseResponseDTO<IEnumerable<DocumentDTO>>>> GetClientDocuments(long clientId)
////{
////    if (clientId <= 0)
////    {
////        return BadRequest(BaseResponseDTO<IEnumerable<DocumentDTO>>.ErrorResult("Invalid client ID"));
////    }

////    var result = await _bankUserService.GetClientDocumentsAsync(clientId);

////    if (result.Success)
////    {
////        return Ok(result);
////    }
////    else
////    {
////        return NotFound(result);
////    }
////}

////// CLIENT USER ENDPOINTS

////[HttpPost("clients/{clientId}/users")]
////public async Task<ActionResult<BaseResponseDTO<ClientUserCreationDTO>>> CreateClientUser(long clientId, [FromBody] RegisterDTO userDTO)
////{
////    if (!ModelState.IsValid)
////    {
////        return BadRequest(BaseResponseDTO<ClientUserCreationDTO>.ErrorResult("Invalid input data"));
////    }

////    userDTO.ClientId = clientId;
////    var result = await _bankUserService.CreateClientUserAsync(userDTO);

////    if (result.Success)
////    {
////        _logger.LogInformation("Client user created successfully for client ID: {ClientId}", clientId);
////        return Ok(result);
////    }
////    else
////    {
////        _logger.LogWarning("Client user creation failed: {Message}", result.Message);
////        return BadRequest(result);
////    }
////}

////[HttpGet("clients/{clientId}/users")]
////public async Task<ActionResult<BaseResponseDTO<IEnumerable<UserDTO>>>> GetClientUsersByClientId(long clientId)
////{
////    if (clientId <= 0)
////    {
////        return BadRequest(BaseResponseDTO<IEnumerable<UserDTO>>.ErrorResult("Invalid client ID"));
////    }

////    var result = await _bankUserService.GetAllClientUsersByClientIdAsync(clientId);

////    if (result.Success)
////    {
////        return Ok(result);
////    }
////    else
////    {
////        return NotFound(result);
////    }
////}

////[HttpGet("clients/{clientId}/users/{userId}")]
////public async Task<ActionResult<BaseResponseDTO<UserDTO>>> GetClientUserById(long clientId, long userId)
////{
////    if (userId <= 0)
////    {
////        return BadRequest(BaseResponseDTO<UserDTO>.ErrorResult("Invalid user ID"));
////    }

////    var result = await _bankUserService.GetClienUserByIdAsync(userId);

////    if (result.Success)
////    {
////        return Ok(result);
////    }
////    else
////    {
////        return NotFound(result);
////    }
////}

////[HttpDelete("clients/{clientId}/users/{userId}")]
////public async Task<ActionResult<BaseResponseDTO<bool>>> DeleteClientUser(long clientId, long userId)
////{
////    if (userId <= 0)
////    {
////        return BadRequest(BaseResponseDTO<bool>.ErrorResult("Invalid user ID"));
////    }

////    var result = await _bankUserService.DeleteClientUserAsync(userId);

////    if (result.Success)
////    {
////        _logger.LogInformation("Client user deleted successfully: {UserId}", userId);
////        return Ok(result);
////    }
////    else
////    {
////        _logger.LogWarning("Client user deletion failed: {Message}", result.Message);
////        return BadRequest(result);
////    }
////}




////    }
////}
//using Banking_Payments.Models.DTO;
//using Banking_Payments.Models;
//using Banking_Payments.Services;
//using Banking_Payments.Models.Enums;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Banking_Payments.DTOs;

//namespace Banking_Payments.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize(Roles = nameof(Role.BankUser))]
//    public class BankUserController : ControllerBase
//    {
//        private readonly IBankUserService _bankUserService;
//        private readonly ILogger<BankUserController> _logger;
//        private readonly IPaymentService _paymentService;

//        public BankUserController(
//            IBankUserService bankUserService,
//            ILogger<BankUserController> logger,
//            IPaymentService paymentService)
//        {
//            _bankUserService = bankUserService;
//            _paymentService = paymentService;
//            _logger = logger;
//            _paymentService = paymentService;
//        }

//        // Helper method to get BankId from claims
//        private int GetBankIdFromClaims()
//        {
//            var bankIdClaim = User.FindFirst("BankId")?.Value;
//            if (string.IsNullOrEmpty(bankIdClaim) || !int.TryParse(bankIdClaim, out int bankId))
//            {
//                throw new UnauthorizedAccessException("BankId claim missing or invalid");
//            }
//            return bankId;
//        }

//        // CLIENT ENDPOINTS

//        [HttpPost("clients")]
//        public async Task<ActionResult<ClientDTO>> CreateClientAsync([FromBody] ClientCreationDTO clientCreationDTO)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var bankId = GetBankIdFromClaims();

//                // Ensure the client is being created for the bank user's bank
//                if (clientCreationDTO.BankId != bankId)
//                {
//                    return Forbid();
//                }

//                var createdClient = await _bankUserService.CreateClientAsync(clientCreationDTO);
//                _logger.LogInformation("Client created successfully: {ClientName}", createdClient.ClientName);
//                return Ok(createdClient);
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                return Unauthorized(new { message = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Client creation failed");
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        [HttpGet("clients")]
//        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAllClients()
//        {
//            try
//            {
//                var bankId = GetBankIdFromClaims();
//                var result = await _bankUserService.GetAllClientsAsync(bankId);
//                return Ok(result);
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                return Unauthorized(new { message = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Failed to retrieve clients");
//                return StatusCode(500, new { message = "An error occurred while retrieving clients." });
//            }
//        }

//        [HttpGet("clients/{clientId}")]
//        public async Task<ActionResult<ClientDTO>> GetClientById(int clientId)
//        {
//            if (clientId <= 0)
//            {
//                return BadRequest(new { message = "Invalid client ID" });
//            }

//            try
//            {
//                var bankId = GetBankIdFromClaims();
//                var result = await _bankUserService.GetClientByIdAsync(clientId, bankId);

//                if (result == null)
//                {
//                    return NotFound(new { message = $"Client with ID {clientId} not found" });
//                }

//                return Ok(result);
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                _logger.LogWarning(ex, "Unauthorized access attempt for client {ClientId}", clientId);
//                return Forbid();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Failed to retrieve client {ClientId}", clientId);
//                return StatusCode(500, new { message = "An error occurred while retrieving the client." });
//            }
//        }

//        [HttpPut("clients/{clientId}")]
//        public async Task<ActionResult<ClientDTO>> UpdateClient(int clientId, [FromBody] ClientDTO clientDTO)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (clientId != clientDTO.ClientId)
//            {
//                return BadRequest(new { message = "Client ID mismatch" });
//            }

//            try
//            {
//                var bankId = GetBankIdFromClaims();
//                var result = await _bankUserService.UpdateClientAsync(clientId, clientDTO, bankId);
//                _logger.LogInformation("Client updated successfully: {ClientId}", clientId);
//                return Ok(result);
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                _logger.LogWarning(ex, "Unauthorized update attempt for client {ClientId}", clientId);
//                return Forbid();
//            }
//            catch (KeyNotFoundException ex)
//            {
//                _logger.LogWarning(ex, "Client not found for update: {ClientId}", clientId);
//                return NotFound(new { message = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Client update failed for {ClientId}", clientId);
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        [HttpDelete("clients/{clientId}")]
//        public async Task<ActionResult<bool>> DeleteClient(int clientId)
//        {
//            if (clientId <= 0)
//            {
//                return BadRequest(new { message = "Invalid client ID" });
//            }

//            try
//            {
//                var bankId = GetBankIdFromClaims();
//                var result = await _bankUserService.DeleteClientAsync(clientId, bankId);

//                if (result)
//                {
//                    _logger.LogInformation("Client deleted successfully: {ClientId}", clientId);
//                    return Ok(new { success = true, message = "Client deleted successfully" });
//                }
//                else
//                {
//                    return BadRequest(new { success = false, message = "Failed to delete client" });
//                }
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                _logger.LogWarning(ex, "Unauthorized delete attempt for client {ClientId}", clientId);
//                return Forbid();
//            }
//            catch (KeyNotFoundException ex)
//            {
//                _logger.LogWarning(ex, "Client not found for deletion: {ClientId}", clientId);
//                return NotFound(new { message = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Client deletion failed for {ClientId}", clientId);
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        // CLIENT VERIFICATION ENDPOINTS

//        [HttpPut("clients/{clientId}/verify")]
//        public async Task<ActionResult<ClientDTO>> VerifyClient(int clientId, [FromBody] ClientVerificationRequestDTO request)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var currentUserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
//                var bankId = GetBankIdFromClaims();

//                if (currentUserId == 0)
//                {
//                    return Unauthorized(new { message = "User authentication information missing" });
//                }

//                var result = await _bankUserService.VerifyClientAsync(
//                    clientId,
//                    currentUserId,
//                    bankId,
//                    request.VerificationStatus,
//                    request.Notes);

//                _logger.LogInformation("Client verification updated: {ClientId}, Status: {Status}",
//                    clientId, request.VerificationStatus);
//                return Ok(result);
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                _logger.LogWarning(ex, "Unauthorized verification attempt for client: {ClientId}", clientId);
//                return Forbid();
//            }
//            catch (KeyNotFoundException ex)
//            {
//                _logger.LogWarning(ex, "Client not found for verification: {ClientId}", clientId);
//                return NotFound(new { message = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Client verification failed for {ClientId}", clientId);
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        [HttpGet("clients/verification-status/{status}")]
//        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetClientsByVerificationStatus(string status)
//        {
//            if (string.IsNullOrEmpty(status) || !Enum.TryParse<VerificationStatus>(status, true, out var parsedStatus))
//            {
//                return BadRequest(new { message = "Invalid verification status" });
//            }

//            try
//            {
//                var bankId = GetBankIdFromClaims();
//                var result = await _bankUserService.GetClientsByVerificationStatusAsync(status, bankId);
//                return Ok(result);
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                return Unauthorized(new { message = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Failed to retrieve clients by verification status: {Status}", status);
//                return StatusCode(500, new { message = "An error occurred while retrieving clients." });
//            }
//        }







//        //// PAYMENT APPROVAL 
//        //[HttpGet("payments/pending")]
//        //public async Task<ActionResult<BaseResponseDTO<IEnumerable<PaymentDTO>>>> GetPendingPayments()
//        //{
//        //    var result = await _paymentService.GetPendingPaymentsAsync();

//        //    if (result.Success)
//        //    {
//        //        return Ok(result);
//        //    }
//        //    else
//        //    {
//        //        return StatusCode(500, result);
//        //    }
//        //}

//        //[HttpGet("payments")]
//        //public async Task<ActionResult<BaseResponseDTO<IEnumerable<PaymentDTO>>>> GetPaymentsByStatus([FromQuery] string status)
//        //{
//        //    if (string.IsNullOrEmpty(status))
//        //    {
//        //        return BadRequest(BaseResponseDTO<IEnumerable<PaymentDTO>>.ErrorResult("Status parameter is required"));
//        //    }

//        //    var result = await _paymentService.GetPaymentsByStatusAsync(status);

//        //    if (result.Success)
//        //    {
//        //        return Ok(result);
//        //    }
//        //    else
//        //    {
//        //        return BadRequest(result);
//        //    }
//        //}
//        //[HttpPost("payments/all")]
//        //public async Task<ActionResult<BaseResponseDTO<IEnumerable<PaymentDTO>>>> GetAllPaymentsByBankUser(long BankId)
//        //{
//        //    if (BankId < 0)
//        //    {
//        //        return BadRequest(BaseResponseDTO<IEnumerable<PaymentDTO>>.ErrorResult("Status parameter is required"));
//        //    }

//        //    var result = await _paymentService.GetAllPaymentsByBankUserId(BankId);

//        //    if (result.Success)
//        //    {
//        //        return Ok(result);
//        //    }
//        //    else
//        //    {
//        //        return BadRequest(result);
//        //    }
//        //}

//        //[HttpPut("payments/{paymentId}/approve")]
//        //public async Task<ActionResult<BaseResponseDTO<PaymentDTO>>> ApprovePayment(long paymentId, [FromBody] ApprovePaymentRequestDTO request)
//        //{
//        //    if (paymentId <= 0)
//        //    {
//        //        return BadRequest(BaseResponseDTO<PaymentDTO>.ErrorResult("Invalid payment ID"));
//        //    }

//        //    var currentBankUserId = long.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

//        //    var result = await _paymentService.ApprovePaymentAsync(paymentId, currentBankUserId, request.Notes);

//        //    if (result.Success)
//        //    {
//        //        _logger.LogInformation("Payment approved successfully: {PaymentId}, ApprovedBy: {BankUserId}", paymentId, currentBankUserId);
//        //        return Ok(result);
//        //    }
//        //    else
//        //    {
//        //        _logger.LogWarning("Payment approval failed: {Message}", result.Message);
//        //        return BadRequest(result);
//        //    }
//        //}

//        //[HttpPut("payments/{paymentId}/reject")]
//        //public async Task<ActionResult<BaseResponseDTO<PaymentDTO>>> RejectPayment(long paymentId, [FromBody] ApprovePaymentRequestDTO request)
//        //{
//        //    if (paymentId <= 0)
//        //    {
//        //        return BadRequest(BaseResponseDTO<PaymentDTO>.ErrorResult("Invalid payment ID"));
//        //    }

//        //    var currentBankUserId = long.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

//        //    var result = await _paymentService.RejectPaymentAsync(paymentId, currentBankUserId, request.Notes);

//        //    if (result.Success)
//        //    {
//        //        _logger.LogInformation("Payment rejected: {PaymentId}, RejectedBy: {BankUserId}", paymentId, currentBankUserId);
//        //        return Ok(result);
//        //    }
//        //    else
//        //    {
//        //        _logger.LogWarning("Payment rejection failed: {Message}", result.Message);
//        //        return BadRequest(result);
//        //    }
//        //}

//        //[HttpGet("payments/{paymentId}")]
//        //public async Task<ActionResult<BaseResponseDTO<PaymentDTO>>> GetPaymentById(long paymentId)
//        //{
//        //    if (paymentId <= 0)
//        //    {
//        //        return BadRequest(BaseResponseDTO<PaymentDTO>.ErrorResult("Invalid payment ID"));
//        //    }

//        //    var result = await _paymentService.GetPaymentByIdAsync(paymentId);

//        //    if (result.Success)
//        //    {
//        //        return Ok(result);
//        //    }
//        //    else
//        //    {
//        //        return NotFound(result);
//        //    }
//        //}

//        //[HttpGet("pending")]
//        //public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPendingPayments()
//        //{
//        //    return await _paymentService.GetPendingPaymentsAsync();
//        //}

//        //[HttpGet("status/{status}")]
//        //public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsByStatus(string status)
//        //{
//        //    return await _paymentService.GetPaymentsByStatusAsync(status);
//        //}

//        //[HttpGet("bank/{bankId:long}")]
//        //public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsByBank(long bankId)
//        //{
//        //    return await _paymentService.GetAllPaymentsByBankUserId(bankId);
//        //}

//        //[HttpGet("{paymentId:long}")]
//        //public async Task<ActionResult<PaymentDTO>> GetPaymentById(long paymentId)
//        //{
//        //    return await _paymentService.GetPaymentByIdAsync(paymentId);
//        //}

//        //[HttpPut("{paymentId:long}/approve/{bankUserId:long}")]
//        //public async Task<ActionResult<PaymentDTO>> ApprovePayment(long paymentId, long bankUserId, [FromBody] ApprovePaymentRequestDTO request)
//        //{
//        //    return await _paymentService.ApprovePaymentAsync(paymentId, bankUserId, request?.Notes);
//        //}

//        //[HttpPut("{paymentId:long}/reject/{bankUserId:long}")]
//        //public async Task<ActionResult<PaymentDTO>> RejectPayment(long paymentId, long bankUserId, [FromBody] ApprovePaymentRequestDTO request)
//        //{
//        //    return await _paymentService.RejectPaymentAsync(paymentId, bankUserId, request?.Notes);
//        //}




//        [HttpGet("pending")]
//        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPendingPayments()
//        {
//            try
//            {
//                var result = await _paymentService.GetPendingPaymentsAsync();
//                return Ok(result);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Failed to retrieve pending payments");
//                return StatusCode(500, new { message = "An error occurred while retrieving pending payments." });
//            }
//        }

//        [HttpGet("status/{status}")]
//        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsByStatus(string status)
//        {
//            if (string.IsNullOrWhiteSpace(status))
//            {
//                return BadRequest(new { message = "Invalid payment status" });
//            }

//            try
//            {
//                var result = await _paymentService.GetPaymentsByStatusAsync(status);
//                return Ok(result);
//            }
//            catch (KeyNotFoundException ex)
//            {
//                _logger.LogWarning(ex, "No payments found with status {Status}", status);
//                return NotFound(new { message = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Failed to retrieve payments by status: {Status}", status);
//                return StatusCode(500, new { message = "An error occurred while retrieving payments." });
//            }
//        }

//        [HttpGet("bank/{bankId:long}")]
//        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPaymentsByBank(long bankId)
//        {
//            if (bankId <= 0)
//            {
//                return BadRequest(new { message = "Invalid bank ID" });
//            }

//            try
//            {
//                var result = await _paymentService.GetAllPaymentsByBankUserId(bankId);
//                return Ok(result);
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                _logger.LogWarning(ex, "Unauthorized access attempt for bank {BankId}", bankId);
//                return Forbid();
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Failed to retrieve payments for bank {BankId}", bankId);
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        [HttpGet("{paymentId:long}")]
//        public async Task<ActionResult<PaymentDTO>> GetPaymentById(long paymentId)
//        {
//            if (paymentId <= 0)
//            {
//                return BadRequest(new { message = "Invalid payment ID" });
//            }

//            try
//            {
//                var result = await _paymentService.GetPaymentByIdAsync(paymentId);
//                return Ok(result);
//            }
//            catch (KeyNotFoundException ex)
//            {
//                _logger.LogWarning(ex, "Payment not found: {PaymentId}", paymentId);
//                return NotFound(new { message = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Failed to retrieve payment {PaymentId}", paymentId);
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        [HttpPut("{paymentId:long}/approve/{bankUserId:long}")]
//        public async Task<ActionResult<PaymentDTO>> ApprovePayment(long paymentId, long bankUserId, [FromBody] ApprovePaymentRequestDTO request)
//        {
//            if (paymentId <= 0 || bankUserId <= 0)
//            {
//                return BadRequest(new { message = "Invalid payment or bank user ID" });
//            }

//            if (request == null)
//            {
//                return BadRequest(new { message = "Approval request body is missing" });
//            }

//            try
//            {
//                var result = await _paymentService.ApprovePaymentAsync(paymentId, bankUserId, request.Notes);
//                _logger.LogInformation("Payment approved: {PaymentId} by {BankUserId}", paymentId, bankUserId);
//                return Ok(result);
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                _logger.LogWarning(ex, "Unauthorized approval attempt: PaymentId={PaymentId}, BankUserId={BankUserId}", paymentId, bankUserId);
//                return Forbid();
//            }
//            catch (KeyNotFoundException ex)
//            {
//                _logger.LogWarning(ex, "Payment not found for approval: {PaymentId}", paymentId);
//                return NotFound(new { message = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Failed to approve payment {PaymentId}", paymentId);
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        [HttpPut("{paymentId:long}/reject/{bankUserId:long}")]
//        public async Task<ActionResult<PaymentDTO>> RejectPayment(long paymentId, long bankUserId, [FromBody] ApprovePaymentRequestDTO request)
//        {
//            if (paymentId <= 0 || bankUserId <= 0)
//            {
//                return BadRequest(new { message = "Invalid payment or bank user ID" });
//            }

//            if (request == null)
//            {
//                return BadRequest(new { message = "Rejection request body is missing" });
//            }

//            try
//            {
//                var result = await _paymentService.RejectPaymentAsync(paymentId, bankUserId, request.Notes);
//                _logger.LogInformation("Payment rejected: {PaymentId} by {BankUserId}", paymentId, bankUserId);
//                return Ok(result);
//            }
//            catch (UnauthorizedAccessException ex)
//            {
//                _logger.LogWarning(ex, "Unauthorized rejection attempt: PaymentId={PaymentId}, BankUserId={BankUserId}", paymentId, bankUserId);
//                return Forbid();
//            }
//            catch (KeyNotFoundException ex)
//            {
//                _logger.LogWarning(ex, "Payment not found for rejection: {PaymentId}", paymentId);
//                return NotFound(new { message = ex.Message });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Failed to reject payment {PaymentId}", paymentId);
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//    }
//}


using Banking_Payments.Models.DTO;
using Banking_Payments.Models;
using Banking_Payments.Services;
using Banking_Payments.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Banking_Payments.DTOs;

namespace Banking_Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            _paymentService = paymentService; // Removed duplicate
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

                // Ensure the client is being created for the bank user's bank
                if (clientCreationDTO.BankId != bankId)
                {
                    return Forbid();
                }

                var createdClient = await _bankUserService.CreateClientAsync(clientCreationDTO);
                _logger.LogInformation("Client created successfully: {ClientName}", createdClient.ClientName);
                return Ok(createdClient);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Client creation failed");
                return BadRequest(new { message = ex.Message });
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
                    request.VerificationStatus,
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