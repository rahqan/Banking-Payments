//using DocumentFormat.OpenXml.Bibliography;
//using Banking_Payments.Models.Enums;

//namespace Banking_Payments.Models.DTO
//{
//    public class PaymentDTO
//    {
//        public int PaymentId { get; set; }
//        public decimal Amount { get; set; }
//        public DateTime PaymentDate { get; set; }
//        public VerificationStatus Status { get; set; }
//        public PaymentType Type { get; set; }
//        public int BeneficiaryId { get; set; }
//        public string? BeneficiaryName { get; set; }
//        public int ClientId { get; set; }
//        public string? ClientName { get; set; }
//        public int? ApprovedById { get; set; }
//        public string? ApprovedByName { get; set; }
//    }
//}


using Banking_Payments.Models;
using Banking_Payments.Models.DTO;
using Banking_Payments.Models.Enums;

public class PaymentDTO
{
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public VerificationStatus Status { get; set; }
    public PaymentType Type { get; set; }
    public int BeneficiaryId { get; set; }

    public string? RejectionRemark { get; set; }
    public BeneficiaryDTO? Beneficiary { get; set; }   // ✅ nested object
    public int ClientId { get; set; }

    public int? BankUserId { get; set; }

    public string? Remarks { get; set; }

    public ClientDTO? Client { get; set; }             // ✅ nested object
    public int? ApprovedById { get; set; }
    public string? ApprovedByName { get; set; }
}
