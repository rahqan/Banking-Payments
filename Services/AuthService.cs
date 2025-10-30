using Banking_Payments.Models;
using Banking_Payments.Models.DTO;
using Banking_Payments.Models.Enums;
using Banking_Payments.Repositories.Interfaces;

namespace Banking_Payments.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly IJwtService _jwtService;

        public AuthService(IAuthRepository authRepo, IJwtService jwtService)
        {
            _authRepo = authRepo;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request)
        {
            switch (request.Role)
            {
                case Role.BankUser:
                    return await LoginBankUserAsync(request);

                case Role.Client:
                    return await LoginClientAsync(request);

                case Role.SuperAdmin:
                    return await LoginAdminAsync(request);

                default:
                    return null;
            }
        }

        private async Task<LoginResponseDTO?> LoginBankUserAsync(LoginRequestDTO request)
        {
            var user = await _authRepo.GetBankUserByEmailAsync(request.Username);
            if (user == null || user.Password != request.Password)
                return null;

            var token = _jwtService.GenerateToken(user.BankUserId, Role.BankUser.ToString(), user.BankId);

            return new LoginResponseDTO
            {
                Token = token,
                Role = Role.BankUser,
                UserId = user.BankUserId
            };
        }

        private async Task<LoginResponseDTO?> LoginClientAsync(LoginRequestDTO request)
        {
            var client = await _authRepo.GetClientByEmailAsync(request.Username);
            if (client == null || client.Password != request.Password)
                return null;

            var token = _jwtService.GenerateToken(client.ClientId, Role.Client.ToString(), client.BankId);

            return new LoginResponseDTO
            {
                Token = token,
                Role = Role.Client,
                UserId = client.ClientId
            };
        }

        private async Task<LoginResponseDTO?> LoginAdminAsync(LoginRequestDTO request)
        {
            var admin = await _authRepo.GetAdminByEmailAsync(request.Username);
            if (admin == null || admin.Password != request.Password)
                return null;

            var token = _jwtService.GenerateToken(admin.AdminId, Role.SuperAdmin.ToString());

            return new LoginResponseDTO
            {
                Token = token,
                Role = Role.SuperAdmin,
                UserId = admin.AdminId
            };
        }
    }
}
