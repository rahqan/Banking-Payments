//namespace Banking_Payments.Models.DTO
//{
//    public class ClientDTO
//    {
//        public long ClientId { get; set; }
//        public string ClientCode { get; set; }
//        public string ClientName { get; set; }
//        public string ClientEmail { get; set; }
//        public int BankId { get; set; }
//        public int BankUserId { get; set; }
//        public string? ClientAddress { get; set; }
//        public string? ClientBusinessType { get; set; }
//        public string ClientVerificationStatus { get; set; }
//        public long? VerifiedBy { get; set; }
//        public DateTime? VerifiedAt { get; set; }
//        public int TotalEmployees { get; set; }
//        public int TotalBeneficiaries { get; set; }
//        public int TotalPayments { get; set; }
//    }
//}


namespace Banking_Payments.Models.DTO
{
    public class ClientDTO
    {
        public int ClientId { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string? ClientBusinessType { get; set; }
        public string? ClientAddress { get; set; }
        public string ClientVerificationStatus { get; set; }
        public int BankId { get; set; }
        public int BankUserId { get; set; }

        // New fields
        public int CreatedBy { get; set; }
        public int? ApprovedBy { get; set; }

        public int? VerifiedBy { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalBeneficiaries { get; set; }
        public int TotalPayments { get; set; }
    }
}