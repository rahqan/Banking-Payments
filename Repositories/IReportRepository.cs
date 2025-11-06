using Banking_Payments.Models.DTO.Reports;

namespace Banking_Payments.Repositories
{
    public interface IReportRepository
    {
        // Super Admin Reports
        Task<SystemOverviewReportDTO> GetSystemOverviewAsync();
        Task<IEnumerable<BankPerformanceReportDTO>> GetBankPerformanceReportAsync(DateTime? startDate, DateTime? endDate);
        Task<TransactionVolumeReportDTO> GetTransactionVolumeReportAsync(DateTime startDate, DateTime endDate);
        Task<FinancialSummaryReportDTO> GetFinancialSummaryReportAsync(DateTime startDate, DateTime endDate);

        // Bank User Reports
        Task<IEnumerable<TransactionReportDTO>> GetTransactionReportByClientAsync(int clientId, int bankId, DateTime? startDate, DateTime? endDate);
        Task<CustomerOnboardingReportDTO> GetCustomerOnboardingReportAsync(int bankId, DateTime? startDate, DateTime? endDate);
        Task<PaymentApprovalReportDTO> GetPaymentApprovalReportAsync(int bankId, DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<ClientActivityReportDTO>> GetClientActivityReportAsync(int bankId, DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<TransactionReportDTO>> GetAllTransactionsByBankAsync(int bankId, DateTime? startDate, DateTime? endDate);
    }
}