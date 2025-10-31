using dummy_api.Models.DTO;

namespace dummy_api.Services
{
    public interface IAuthService
    {
          Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request);


        Task<RegistrationResponseDTO> RegisterAdminAsync(RegisterAdminDTO request);
        Task<RegistrationResponseDTO> RegisterBankUserAsync(RegisterBankUserDTO request);
        Task<RegistrationResponseDTO> RegisterClientAsync(RegisterClientDTO request, int bankUserId);



    }
}
