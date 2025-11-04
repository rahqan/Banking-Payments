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

        public SuperAdminController(IBankService bankService)
        {
            _bankService = bankService;
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
        public async Task<IActionResult> GetAll()
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
        public async Task<IActionResult> Create([FromBody] CreateBankDTO dto)
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
    }
}

