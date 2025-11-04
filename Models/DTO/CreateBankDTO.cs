using System.ComponentModel.DataAnnotations;

namespace Banking_Payments.Models.DTOs
{
    public class CreateBankDTO
    {
        [Required]
        public string Code { get; set; }

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

        [Required]
        public int CreatedByAdminId { get; set; }
    }
}

