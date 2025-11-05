using Banking_Payments.Models.DTO;
using Banking_Payments.Models.DTOs;

namespace Banking_Payments.Services
{
    public interface ISuperAdminService
    {
        // BANK USER CRUD
        Task<IEnumerable<BankUserDTO>> GetAllBankUsersAsync();
        Task<BankUserDTO?> GetBankUserByIdAsync(int id);
        Task<BankUserDTO> CreateBankUserAsync(CreateBankUserDTO dto);
        Task<bool> UpdateBankUserAsync(int id, UpdateBankUserDTO dto);
        Task<bool> DeleteBankUserAsync(int id);
    }
}
