using Banking_Payments.Models;
using Banking_Payments.Models.DTO;
using Banking_Payments.Repositories;
using CsvHelper;
using CsvHelper.Configuration;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Banking_Payments.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Client>> GetAllClientByBankIdAsync(int id)
        {
            return await clientRepository.GetAllClientByBankIdAsync(id);
        }

        public async Task<bool> DeleteEmployeeByEmployeeId(int employeeId)
        {
            return await clientRepository.DeleteEmployeeByEmployeeId(employeeId);
        }

        public async Task<ClientBankDetailsDTO> GetClientForBankDetailsAsync(int clientId)
        {
            return await clientRepository.GetClientForBankDetailsAsync(clientId);
        }

        public async Task<IEnumerable<Client>> GetClientsAllAsync(int id)
        {
            return await clientRepository.GetClientsAllAsync(id);
        }

        public async Task<SalaryDisbursement> AddSalaryDisbursementIndividuallyAsync(SalaryDisbursement s)
        {
            return await clientRepository.AddSalaryDisbursementIndividuallyAsync(s);
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

       

        public async Task<List<Employee>> ReadEmployeesFromExcel(Stream file)
        {
            var employees = new List<Employee>();

            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim,
                MissingFieldFound = null,
                BadDataFound = null
            }))
            {
                await csv.ReadAsync();
                csv.ReadHeader();

                int row = 2;

                while (await csv.ReadAsync())
                {
                    try
                    {
                        var employeeCode = csv.GetField<string>(0)?.Trim();

                        if (string.IsNullOrEmpty(employeeCode))
                        {
                            row++;
                            continue;
                        }

                        var salaryValue = csv.GetField<string>(2)?.Trim();
                        if (string.IsNullOrEmpty(salaryValue))
                        {
                            throw new Exception($"Invalid salary value at row {row}");
                        }

                        if (!decimal.TryParse(salaryValue, out var salary))
                        {
                            throw new Exception($"Invalid salary value at row {row}");
                        }

                        var emp = new Employee
                        {
                            EmployeeCode = employeeCode,
                            Name = csv.GetField<string>(1)?.Trim(),
                            Salary = salary,
                            AccountNumber = csv.GetField<string>(3)?.Trim(),
                            IfscCode = csv.GetField<string>(4)?.Trim()
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
           

            foreach (var emp in employees)
            {
                if (string.IsNullOrEmpty(emp.EmployeeCode) || string.IsNullOrEmpty(emp.Name) || emp.Salary <= 0)
                {
                    throw new Exception("EmployeeCode, Name, and Salary are required fields and must be valid.");
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
                int processedCount = 0;
                int skippedCount = 0;
                List<string> errors = new List<string>();

                while (start < totalEmployee)
                {
                    for (int i = 0; i < batchNumber && start < totalEmployee; i++)
                    {
                        var employee = employees.ElementAt(start);

                        try
                        {
                            if (!clientRepository.CheckEmployeeExisByCode(employee.EmployeeCode, clientId))
                            {
                                employee.ClientId = clientId;
                                var addedEmployee = clientRepository.AddEmployeeAsync(employee).Result;
                                employee.EmployeeId = addedEmployee.EmployeeId;
                            }

                            clientRepository.SalaryDisbursementByBatch(employee, clientId);
                            processedCount++;
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Employee {employee.EmployeeCode}: {ex.Message}");
                            skippedCount++;
                        }

                        start++;
                    }
                }

                var res = new
                {
                    Success = errors.Count == 0,
                    TotalEmployees = totalEmployee,
                    ProcessedCount = processedCount,
                    SkippedCount = skippedCount,
                    TotalBatches = (totalEmployee / batchNumber) + 1,
                    Errors = errors
                };
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong in batch processing: {ex.Message}", ex);
            }
        }
    }
}