using Banking_Payments.Models.DTO;
using Banking_Payments.Models.DTOs;
using Banking_Payments.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banking_Payments.Controllers
{
    [Route("api/Bank")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : ControllerBase
    {
        private readonly IBankService _bankService;
        private readonly ISuperAdminService _superAdminService;

        public SuperAdminController(IBankService bankService, ISuperAdminService superAdminService)
        {
            _bankService = bankService;
            _superAdminService = superAdminService;
        }

        private int GetAdminIdFromClaims()
        {
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            if (role != "SuperAdmin")
                throw new UnauthorizedAccessException("Only SuperAdmin can perform this action");

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int adminId))
                throw new UnauthorizedAccessException("UserId claim missing or invalid");

            return adminId;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllBanks()
        {
            var banks = await _bankService.GetAllAsync();
            return Ok(banks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBankById(int id)
        {
            var bank = await _bankService.GetByIdAsync(id);
            if (bank == null)
                return NotFound(new { message = "Bank not found" });

            return Ok(bank);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanks([FromBody] CreateBankDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var adminId = GetAdminIdFromClaims();
            var bank = await _bankService.CreateAsync(dto, adminId);

            return CreatedAtAction(nameof(GetBankById), new { id = bank.BankId }, bank);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBank(int id, [FromBody] UpdateBankDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _bankService.UpdateAsync(id, dto);
            if (!success) return NotFound(new { message = "Bank not found" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteBank(int id)
        {
            var success = await _bankService.SoftDeleteAsync(id);
            if (!success) return NotFound(new { message = "Bank not found" });

            return Ok(new { message = "Bank soft deleted successfully" });
        }

        [HttpGet("bankUsers")]
        public async Task<IActionResult> GetAllBankUsers()
        {
            var users = await _superAdminService.GetAllBankUsersAsync();
            return Ok(users);
        }

        [HttpGet("bankUsers/{id}")]
        public async Task<IActionResult> GetBankUserById(int id)
        {
            var user = await _superAdminService.GetBankUserByIdAsync(id);
            if (user == null) return NotFound(new { message = "Bank user not found" });
            return Ok(user);
        }

        [HttpPost("bankUsers")]
        public async Task<IActionResult> CreateBankUser([FromBody] CreateBankUserDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _superAdminService.CreateBankUserAsync(dto);
            return CreatedAtAction(nameof(GetBankUserById), new { id = created.BankUserId }, created);
        }

        [HttpPut("bankUsers/{id}")]
        public async Task<IActionResult> UpdateBankUser(int id, [FromBody] UpdateBankUserDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _superAdminService.UpdateBankUserAsync(id, dto);
            if (!success) return NotFound(new { message = "Bank user not found" });

            return NoContent();
        }

        [HttpDelete("bankUsers/{id}")]
        public async Task<IActionResult> DeleteBankUser(int id)
        {
            var success = await _superAdminService.DeleteBankUserAsync(id);
            if (!success) return NotFound(new { message = "Bank user not found" });

            return Ok(new { message = "Bank user deleted successfully" });
        }

    }
}

