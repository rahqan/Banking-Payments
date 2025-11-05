using Banking_Payments.Models;
using Banking_Payments.Models.DTO;
using Banking_Payments.Models.DTOs;
using Banking_Payments.Repositories;

namespace Banking_Payments.Services
{
    public class SuperAdminService : ISuperAdminService
    {
        private readonly ISuperAdminRepository _repo;

        public SuperAdminService(ISuperAdminRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<BankUserDTO>> GetAllBankUsersAsync()
        {
            var users = await _repo.GetAllBankUsersAsync();
            return users.Select(MapToDTO);
        }

        public async Task<BankUserDTO?> GetBankUserByIdAsync(int id)
        {
            var user = await _repo.GetBankUserByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<BankUserDTO> CreateBankUserAsync(CreateBankUserDTO dto)
        {
            var user = new BankUser
            {
                Code = dto.Code,
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password, // hash later
                PhoneNumber = dto.PhoneNumber,
                BankId = dto.BankId
            };

            await _repo.AddBankUserAsync(user);
            await _repo.SaveChangesAsync();

            return MapToDTO(user);
        }

        public async Task<bool> UpdateBankUserAsync(int id, UpdateBankUserDTO dto)
        {
            var user = await _repo.GetBankUserByIdAsync(id);
            if (user == null) return false;

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;

            if (dto.ResetPassword == true && !string.IsNullOrEmpty(dto.NewPassword))
                user.Password = dto.NewPassword; // hash later

            await _repo.UpdateBankUserAsync(user);
            await _repo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteBankUserAsync(int id)
        {
            var existing = await _repo.GetBankUserByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteBankUserAsync(id);
            await _repo.SaveChangesAsync();
            return true;
        }

        private static BankUserDTO MapToDTO(BankUser user)
        {
            return new BankUserDTO
            {
                BankUserId = user.BankUserId,
                Code = user.Code,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BankId = user.BankId
            };
        }
    }
}
