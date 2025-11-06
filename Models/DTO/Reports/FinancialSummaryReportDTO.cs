namespace Banking_Payments.Models.DTO.Reports
{
    public class FinancialSummaryReportDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalMoneyFlow { get; set; }
        public decimal TotalPaymentsValue { get; set; }
        public decimal TotalSalariesValue { get; set; }
        public decimal TotalClientBalance { get; set; }
        public List<BankFinancialSummary> BankWiseFinancials { get; set; }
        public List<TopClient> TopClientsByVolume { get; set; }
        public MonthlyFinancialTrend MonthlyTrend { get; set; }
    }

    public class BankFinancialSummary
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public int ClientCount { get; set; }
        public decimal TotalPaymentValue { get; set; }
        public decimal TotalSalaryValue { get; set; }
        public decimal AverageTransactionValue { get; set; }
        public decimal TotalClientBalance { get; set; }
    }

    public class TopClient
    {
        public int ClientId { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string BankName { get; set; }
        public int TransactionCount { get; set; }
        public decimal TotalTransactionValue { get; set; }
    }

    public class MonthlyFinancialTrend
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal TotalSalaries { get; set; }
    }
}