using Banking_Payments.Models.DTO;
using Banking_Payments.Models.DTOs;

namespace Banking_Payments.Services
{
    public interface IBankService
    {
        Task<IEnumerable<BankDTO>> GetAllAsync();
        Task<BankDTO?> GetByIdAsync(int id);
        Task<BankDTO> CreateAsync(CreateBankDTO dto, int createdByAdminId);

        Task<bool> UpdateAsync(int id, UpdateBankDTO dto);
        Task<bool> SoftDeleteAsync(int id);

        Task<List<BankWithClientsDTO>> GetAllWithClientCountAsync();
    }
}
