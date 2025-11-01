using DocumentFormat.OpenXml.Spreadsheet;
using Banking_Payments.Services;
using Banking_Payments.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Banking_Payments.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var result = await _authService.LoginAsync(request);

            if (result == null)
                return Unauthorized("Invalid credentials or role");

            return Ok(result);
        }

        // Only accessible without authentication (for initial admin setup)
        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminDTO request)
        {
            var result = await _authService.RegisterAdminAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // Only Admin can create BankUsers
        [HttpPost("register/bankuser")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> RegisterBankUser([FromBody] RegisterBankUserDTO request)
        {
            var result = await _authService.RegisterBankUserAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // Only BankUser can create Clients
        [HttpPost("register/client")]
        [Authorize(Roles = "BankUser")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterClientDTO request)
        {
            // Get BankUserId from JWT claims
            var bankUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(bankUserIdClaim) || !int.TryParse(bankUserIdClaim, out int bankUserId))
            {
                return Unauthorized("Invalid token");
            }

            var result = await _authService.RegisterClientAsync(request, bankUserId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
