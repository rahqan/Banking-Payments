//namespace Banking_Payments.Models.DTO
//{
//    public class ClientCreationDTO
//    {
//        public string ClientName { get; set; }

//        //public string ClientCode { get; set; }
//        public string ClientEmail { get; set; }

//        public string ClientPassword { get; set; }
//        public int BankId { get; set; }
//        public int BankUserId { get; set; }
//        public string? Address { get; set; }
//        public string? ClientBusinessType { get; set; }
//    }
//}


using System.ComponentModel.DataAnnotations;

namespace Banking_Payments.Models.DTO
{
    public class ClientCreationDTO
    {
        [Required(ErrorMessage = "Client name is required")]
        [StringLength(100, ErrorMessage = "Client name cannot exceed 100 characters")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Client password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
        public string ClientPassword { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string ClientEmail { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string? Address { get; set; }

        [StringLength(100, ErrorMessage = "Business type cannot exceed 100 characters")]
        public string? ClientBusinessType { get; set; }

        public ClientBankDetailsDTO? BankDetails { get; set; }
    }
}