using Banking_Payments.Models;
using Banking_Payments.Models.DTO;

namespace Banking_Payments.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> SearchClientAsync(string search = "");
        Task<ClientBankDetailsDTO> GetClientForBankDetailsAsync(int clientId);
        Task<IEnumerable<Client>> GetAllClientByBankIdAsync(int id);
        Task<IEnumerable<Client>> GetClientsAllAsync(int id);
        Task<bool> DeleteEmployeeByEmployeeId(int employeeId);
        Task<SalaryDisbursement> AddSalaryDisbursementIndividuallyAsync(SalaryDisbursement s);
        Task<Client> GetClientByRegisterationNumberAsync(string registerationNumber, int bankId);
        Task<IEnumerable<Client>> GetClientsWithStatusAsync(string status, int bankId);
        Task<Beneficiary> AddBeneficiaryAsync(Beneficiary beneficiary);
        Task<Beneficiary> UpdateBeneficiaryAsync(Beneficiary beneficiary);
        Task<bool> DeleteBeneficiaryAsync(int id);
        Task<Beneficiary> ShowBeneficiaryByIdAsync(int id);
        Task<IEnumerable<Beneficiary>> ShowBeneficiariesAsync(int clientId);
        Task<Payment> AddPaymentAsync(Payment payment);
        Task<Payment> ShowPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> ShowAllPaymentsAsync(int clientId);
        Task<Employee> AddEmployeeAsync(Employee emp);
        Task<Employee> ShowEmployeeByIdAsync(int id);
        Task<Object> ShowAllEmployeeAsync(int clientId, int pageNumber, int pageSize);
        Task<IEnumerable<SalaryDisbursement>> ShowSalaryDisbursementByEmpIdAsync(int empId);
        Task<IEnumerable<SalaryDisbursement>> ShowSalaryDisbursementByClientIdAsync(int clientId);
        bool CheckEmployeeExisById(int empId, int clientId);
        bool CheckEmployeeExisByCode(string empCode, int clientId);
        bool SalaryDisbursementByBatch(Employee emp, int clientId);
    }
}
