using Banking_Payments.Models;
using Banking_Payments.Models.DTO;
using Banking_Payments.Repositories;
using OfficeOpenXml.Core.ExcelPackage;

namespace Banking_Payments.Services
{
    public class ClientService : IClientService
    {
        public readonly IClientRepository clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Client>> GetAllClientByBankIdAsync(int id)
        {
            return await clientRepository.GetAllClientByBankIdAsync(id);
        }

        public async Task<ClientBankDetailsDTO> GetClientForBankDetailsAsync(int clientId)
        {
            return await clientRepository.GetClientForBankDetailsAsync(clientId);
        }

        public async Task<IEnumerable<Client>> GetClientsAllAsync(int id)
        {
            return await clientRepository.GetClientsAllAsync(id);
        }

        public async Task<Client> GetClientByRegisterationNumberAsync(string registerationNumber, int bankId)
        {
            return await clientRepository.GetClientByRegisterationNumberAsync(registerationNumber, bankId);
        }

        public async Task<IEnumerable<Client>> GetClientsWithStatusAsync(string status, int bankId)
        {
            return await clientRepository.GetClientsWithStatusAsync(status, bankId);
        }

        public async Task<Beneficiary> AddBeneficiaryAsync(Beneficiary beneficiary)
        {
            return await clientRepository.AddBeneficiaryAsync(beneficiary);
        }

        public async Task<Beneficiary> UpdateBeneficiaryAsync(Beneficiary beneficiary)
        {
            return await clientRepository.UpdateBeneficiaryAsync(beneficiary);
        }

        public async Task<bool> DeleteBeneficiaryAsync(int id)
        {
            return await clientRepository.DeleteBeneficiaryAsync(id);
        }

        public async Task<Beneficiary> ShowBeneficiaryByIdAsync(int id)
        {
            return await clientRepository.ShowBeneficiaryByIdAsync(id);
        }

        public async Task<IEnumerable<Beneficiary>> ShowBeneficiariesAsync(int clientId)
        {
            return await clientRepository.ShowBeneficiariesAsync(clientId);
        }

        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            return await clientRepository.AddPaymentAsync(payment);
        }

        public async Task<Payment> ShowPaymentByIdAsync(int id)
        {
            return await clientRepository.ShowPaymentByIdAsync(id);
        }

        public async Task<IEnumerable<Payment>> ShowAllPaymentsAsync(int clientId)
        {
            return await clientRepository.ShowAllPaymentsAsync(clientId);
        }

        public async Task<Employee> AddEmployeeAsync(Employee emp)
        {
            return await clientRepository.AddEmployeeAsync(emp);
        }

        public async Task<Employee> ShowEmployeeByIdAsync(int id)
        {
            return await clientRepository.ShowEmployeeByIdAsync(id);
        }

        public async Task<Object> ShowAllEmployeeAsync(int clientId, int pageNumber, int pageSize)
        {
            return await clientRepository.ShowAllEmployeeAsync(clientId, pageNumber, pageSize);
        }

        public async Task<IEnumerable<SalaryDisbursement>> ShowSalaryDisbursementByEmpIdAsync(int empId)
        {
            return await clientRepository.ShowSalaryDisbursementByEmpIdAsync(empId);
        }

        public async Task<IEnumerable<SalaryDisbursement>> ShowSalaryDisbursementByClientIdAsync(int clientId)
        {
            return await clientRepository.ShowSalaryDisbursementByClientIdAsync(clientId);
        }
        public async Task<SalaryDisbursement> AddSalaryDisbursementIndividuallyAsync(SalaryDisbursement s)
        {
            return await clientRepository.AddSalaryDisbursementIndividuallyAsync(s);
        }
        public async Task<List<Employee>> ReadEmployeesFromExcel(Stream file)
        {
            var employees = new List<Employee>();

            using (var package = new ExcelPackage(file))
            {
                if (package.Workbook.Worksheets.Count == 0)
                {
                    throw new Exception("No worksheet found in the Excel file.");
                }
                var worksheet = package.Workbook.Worksheets[0];
                int row = 2, maxRows = 10000, emptyRowCount = 0;

                while (row <= maxRows)
                {
                    var employeeCode = worksheet.Cell(row, 1).Value?.ToString()?.Trim();
                    if (string.IsNullOrEmpty(employeeCode))
                    {
                        emptyRowCount++;
                        if (emptyRowCount >= 5) break;
                        row++;
                        continue;
                    }
                    emptyRowCount = 0;
                    try
                    {
                        var salaryValue = worksheet.Cell(row, 3).Value?.ToString()?.Trim();
                        if (string.IsNullOrEmpty(salaryValue))
                        {
                            throw new Exception($"Invalid salary value at row {row}");
                        }
                        decimal salary = 0;
                        if (!decimal.TryParse(salaryValue, out salary))
                        {
                            throw new Exception($"Invalid salary value at row {row}");
                        }

                        var emp = new Employee
                        {
                            EmployeeCode = employeeCode,
                            Name = worksheet.Cell(row, 2).Value?.ToString()?.Trim(),
                            Salary = salary,
                            AccountNumber = worksheet.Cell(row, 4).Value?.ToString()?.Trim(),
                            IfscCode = worksheet.Cell(row, 5).Value?.ToString().Trim()
                        };

                        employees.Add(emp);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error reading row {row}: {ex.Message}");
                    }

                    row++;
                }

            }
            return employees;
        }

        public Object SalaryDisbursementByBatch(List<Employee> employees, int batchNumber, int clientId)
        {
            try
            {
                int totalEmployee = employees.Count;
                int start = 0;
                while (start < totalEmployee)
                {
                    for (int i = 0; i < batchNumber && start < totalEmployee; i++)
                    {
                        if (!clientRepository.CheckEmployeeExisByCode(employees.ElementAt(start).EmployeeCode, clientId))
                        {
                            throw new Exception("Invalid Employee code.");
                        }
                        clientRepository.SalaryDisbursementByBatch(employees.ElementAt(start), clientId);
                        start++;
                    }
                }

                var res = new { Success = true, TotalEmployees = totalEmployee, TotalBatches = (totalEmployee / batchNumber) + 1 };
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong {ex}");
            }
        }
    }
}
