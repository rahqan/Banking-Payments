using Banking_Payments.Models.DTO;
using Banking_Payments.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}
