// RegisterAdminDTO.cs
namespace Banking_Payments.Models.DTO
{
    public class RegisterAdminDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

// RegisterBankUserDTO.cs
namespace Banking_Payments.Models.DTO
{
    public class RegisterBankUserDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int BankId { get; set; }
    }
}

// RegisterClientDTO.cs
namespace Banking_Payments.Models.DTO
{
    public class RegisterClientDTO
    {
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string Password { get; set; }
        public string ClientBusinessType { get; set; }
        public string ClientAddress { get; set; }
        public int BankId { get; set; }
        // BankUserId will come from JWT Claims
    }
}

// RegistrationResponseDTO.cs
namespace Banking_Payments.Models.DTO
{
    public class RegistrationResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? UserId { get; set; }
    }
}
