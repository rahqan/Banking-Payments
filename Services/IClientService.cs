using dummy_api.Models;

namespace dummy_api.Services
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllClientByBankIdAsync(int bankId);
        Task<IEnumerable<Client>> GetClientsAllAsync(int bankId);
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
        public Task<List<Employee>> ReadEmployeesFromExcel(Stream file);
        public Object SalaryDisbursementByBatch(List<Employee> employees, int batchNumber, int clientId);
    }
}
