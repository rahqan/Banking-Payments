using Banking_Payments.Models;
using Banking_Payments.Models.DTO;
using Banking_Payments.Models.Enums;
using Banking_Payments.Repositories;

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

        // Existing Login methods...
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
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
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
            if (client == null || !BCrypt.Net.BCrypt.Verify(request.Password, client.Password))
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
            if (admin == null || !BCrypt.Net.BCrypt.Verify(request.Password, admin.Password))
                return null;

            var token = _jwtService.GenerateToken(admin.AdminId, Role.SuperAdmin.ToString());

            return new LoginResponseDTO
            {
                Token = token,
                Role = Role.SuperAdmin,
                UserId = admin.AdminId
            };
        }

        // Registration methods
        public async Task<RegistrationResponseDTO> RegisterAdminAsync(RegisterAdminDTO request)
        {
            // Validate email uniqueness
            if (await _authRepo.EmailExistsAsync(request.Email))
            {
                return new RegistrationResponseDTO
                {
                    Success = false,
                    Message = "Email already exists"
                };
            }

            // Validate code uniqueness
            if (await _authRepo.CodeExistsAsync(request.Code, "Admin"))
            {
                return new RegistrationResponseDTO
                {
                    Success = false,
                    Message = "Admin code already exists"
                };
            }

            // Hash password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var admin = new Admin
            {
                Code = request.Code,
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword
            };

            var createdAdmin = await _authRepo.CreateAdminAsync(admin);

            return new RegistrationResponseDTO
            {
                Success = true,
                Message = "Admin registered successfully",
                UserId = createdAdmin.AdminId
            };
        }

        public async Task<RegistrationResponseDTO> RegisterBankUserAsync(RegisterBankUserDTO request)
        {
            // Validate email uniqueness
            if (await _authRepo.EmailExistsAsync(request.Email))
            {
                return new RegistrationResponseDTO
                {
                    Success = false,
                    Message = "Email already exists"
                };
            }

            // Validate code uniqueness
            if (await _authRepo.CodeExistsAsync(request.Code, "BankUser"))
            {
                return new RegistrationResponseDTO
                {
                    Success = false,
                    Message = "BankUser code already exists"
                };
            }

            // Validate bank exists
            if (!await _authRepo.BankExistsAsync(request.BankId))
            {
                return new RegistrationResponseDTO
                {
                    Success = false,
                    Message = "Invalid Bank ID"
                };
            }

            // Hash password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var bankUser = new BankUser
            {
                Code = request.Code,
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword,
                PhoneNumber = request.PhoneNumber,
                BankId = request.BankId
            };

            var createdBankUser = await _authRepo.CreateBankUserAsync(bankUser);

            return new RegistrationResponseDTO
            {
                Success = true,
                Message = "BankUser registered successfully",
                UserId = createdBankUser.BankUserId
            };
        }

        public async Task<RegistrationResponseDTO> RegisterClientAsync(RegisterClientDTO request, int bankUserId)
        {
            // Validate email uniqueness
            if (await _authRepo.EmailExistsAsync(request.ClientEmail))
            {
                return new RegistrationResponseDTO
                {
                    Success = false,
                    Message = "Email already exists"
                };
            }

            // Validate code uniqueness
            if (await _authRepo.CodeExistsAsync(request.ClientCode, "Client"))
            {
                return new RegistrationResponseDTO
                {
                    Success = false,
                    Message = "Client code already exists"
                };
            }

            // Validate bank exists
            if (!await _authRepo.BankExistsAsync(request.BankId))
            {
                return new RegistrationResponseDTO
                {
                    Success = false,
                    Message = "Invalid Bank ID"
                };
            }

            // Hash password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var client = new Client
            {
                Code = request.ClientCode,
                Name = request.ClientName,
                Email = request.ClientEmail,
                Password = hashedPassword,
                BusinessType = request.ClientBusinessType,
                Address = request.ClientAddress,
                BankId = request.BankId,
                BankUserId = bankUserId, // From JWT Claims
                VerificationStatus = "Pending", // Default status
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var createdClient = await _authRepo.CreateClientAsync(client);

            return new RegistrationResponseDTO
            {
                Success = true,
                Message = "Client registered successfully",
                UserId = createdClient.ClientId
            };
        }
    }
}
