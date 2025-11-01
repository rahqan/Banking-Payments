using Banking_Payments.Models.DTO;

namespace Banking_Payments.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request);
        Task<RegistrationResponseDTO> RegisterAdminAsync(RegisterAdminDTO request);
        Task<RegistrationResponseDTO> RegisterBankUserAsync(RegisterBankUserDTO request);
        Task<RegistrationResponseDTO> RegisterClientAsync(RegisterClientDTO request, int bankUserId);
    }
}
