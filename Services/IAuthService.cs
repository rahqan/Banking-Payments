using Banking_Payments.Models.DTO;

namespace Banking_Payments.Services
{
    public interface IAuthService
    {
          Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request);
       
    }
}
