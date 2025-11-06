namespace Banking_Payments.Models.DTO.Reports
{
    public class ClientActivityReportDTO
    {
        public int ClientId { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int TotalPayments { get; set; }
        public decimal TotalPaymentValue { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalBeneficiaries { get; set; }
        public int TotalSalaryDisbursements { get; set; }
        public decimal TotalSalaryValue { get; set; }
        public DateTime LastActivityDate { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}