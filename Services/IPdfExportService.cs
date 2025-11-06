using Banking_Payments.Models.DTO.Reports;

namespace Banking_Payments.Services
{
    public interface IPdfExportService
    {
        byte[] GenerateSystemOverviewPdf(SystemOverviewReportDTO report);
        byte[] GenerateBankPerformancePdf(IEnumerable<BankPerformanceReportDTO> report, DateTime? startDate, DateTime? endDate);
        byte[] GenerateTransactionVolumePdf(TransactionVolumeReportDTO report);
        byte[] GenerateFinancialSummaryPdf(FinancialSummaryReportDTO report);
        byte[] GenerateTransactionReportPdf(IEnumerable<TransactionReportDTO> transactions, string clientName, DateTime? startDate, DateTime? endDate);
        byte[] GenerateCustomerOnboardingPdf(CustomerOnboardingReportDTO report);
        byte[] GeneratePaymentApprovalPdf(PaymentApprovalReportDTO report);
        byte[] GenerateClientActivityPdf(IEnumerable<ClientActivityReportDTO> report);
    }
}