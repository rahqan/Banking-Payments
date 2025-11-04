using System.ComponentModel.DataAnnotations;

namespace Banking_Payments.Models.DTOs
{
    public class UpdateBankDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PanNumber { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public bool IsActive { get; set; }
    }
}
