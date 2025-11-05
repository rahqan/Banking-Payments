namespace Banking_Payments.Models.DTO
{
    public class BankWithClientsDTO
    {
        public int BankId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PanNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedByAdminId { get; set; }
        public int ClientCount { get; set; }
    }
}