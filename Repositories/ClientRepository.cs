using DocumentFormat.OpenXml.InkML;
using dummy_api.Context;
using dummy_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace dummy_api.Repositories
{
    public class ClientRepository : IClientRepository
    {
        public readonly AppDbContext _appDbContext;
        public ClientRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Client>> SearchClientAsync(string search = "")
        {
            var query = _appDbContext.Clients.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Name.Contains(search));
            }
            
            var res = await query.ToListAsync();
            return res;
        }

        public async Task<IEnumerable<Client>> GetAllClientByBankIdAsync(int bankId)
        {
            var res = await _appDbContext.Clients
                        .Where(c => c.BankId == bankId)
                        .Include(c => c.Bank)
                        .ToListAsync();
            return res;
        }

        public async Task<IEnumerable<Client>> GetClientsAllAsync(int bankId)
        {
            return await _appDbContext.Clients
                .Where(c => c.BankId == bankId)
                .Include(c => c.Bank)
                .Include(c => c.Beneficiaries)
                .Include(c => c.Employees)
                .Include(c => c.Payments)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<Client> GetClientByRegisterationNumberAsync(string registerationNumber, int bankId)
        {
            return await _appDbContext.Clients.Include(c=>c.Bank).FirstOrDefaultAsync(c => c.RegisterationNumber == registerationNumber && c.BankId == bankId);
        }

        public async Task<IEnumerable<Client>> GetClientsWithStatusAsync(string status, int bankId)
        {
            return await _appDbContext.Clients
                    .Where(c => c.VerificationStatus == status)
                    .Include(c => c.Bank)
                    .OrderBy(c => c.CreatedAt)
                    .ToListAsync();
        }

        public async Task<Beneficiary> AddBeneficiaryAsync(Beneficiary beneficiary)
        {
            await _appDbContext.Beneficiaries.AddAsync(beneficiary);
            await _appDbContext.SaveChangesAsync();
            return beneficiary;
        }

        public async Task<Beneficiary> UpdateBeneficiaryAsync(Beneficiary beneficiary)
        {
            Beneficiary ben = await _appDbContext.Beneficiaries.FirstOrDefaultAsync(b => b.BeneficiaryId == beneficiary.BeneficiaryId);
            if(ben != null)
            {
                ben.Name = beneficiary.Name;
                ben.AccountNumber = beneficiary.AccountNumber;
                ben.IfscCode = beneficiary.IfscCode;
                await _appDbContext.SaveChangesAsync();
            }
            return ben;
        }

        public async Task<bool> DeleteBeneficiaryAsync(int id)
        {
            Beneficiary ben = await _appDbContext.Beneficiaries.FirstOrDefaultAsync(b => b.BeneficiaryId == id);
            if(ben != null)
            {
                _appDbContext.Beneficiaries.Remove(ben);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Beneficiary> ShowBeneficiaryByIdAsync(int id)
        {
            Beneficiary ben = await _appDbContext.Beneficiaries
                        .Include(b => b.Client)
                        .FirstOrDefaultAsync(b => b.BeneficiaryId == id);
            return ben;
        }

        public async Task<IEnumerable<Beneficiary>> ShowBeneficiariesAsync(int clientId)
        {
            return await _appDbContext.Beneficiaries
                        .Where(b => b.ClientId == clientId)
                        .Include(b=>b.Client)
                        .ToListAsync();
        }

        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            await _appDbContext.Payments.AddAsync(payment);
            await _appDbContext.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment> ShowPaymentByIdAsync(int id)
        {
            Payment res = await _appDbContext.Payments.FirstOrDefaultAsync(p => p.PaymentId == id);
            return res;
        }

        public async Task<IEnumerable<Payment>> ShowAllPaymentsAsync(int clientId)
        {
            return await _appDbContext.Payments
                        .Where(p => p.ClientId == clientId)
                        .Include(p=>p.Beneficiary)
                        .ToListAsync();
        }

        public async Task<Employee> AddEmployeeAsync(Employee emp)
        {
            await _appDbContext.Employees.AddAsync(emp);
            await _appDbContext.SaveChangesAsync();
            return emp;
        }

        public async Task<Employee> ShowEmployeeByIdAsync(int id)
        {
            return await _appDbContext.Employees.FirstOrDefaultAsync(s => s.EmployeeId == id);
        }

        public async Task<Object> ShowAllEmployeeAsync(int clientId, int pageNumber, int pageSize)
        {
            var filter = new PaginationFilter(pageNumber, pageSize);
            var employees = await _appDbContext.Employees
                        .Where(e => e.ClientId == clientId)
                        .Skip((filter.PageNumber - 1) * filter.PageSize)
                        .Take(pageSize)
                        .ToListAsync();

            var totalRecords = await _appDbContext.Employees.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize);

            var res = new
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                employees = employees
            };
            return res;
        }

        public async Task<IEnumerable<SalaryDisbursement>> ShowSalaryDisbursementByEmpIdAsync(int empId)
        {
            return await _appDbContext.SalaryDisbursements
                            .Where(s => s.EmployeeId == empId)
                            .Include(s => s.Employee)
                            .Include(s => s.Client)
                            .OrderByDescending(s => s.CreatedAt)
                            .ToListAsync();
        }

        public async Task<IEnumerable<SalaryDisbursement>> ShowSalaryDisbursementByClientIdAsync(int clientId)
        {
            return await _appDbContext.SalaryDisbursements
                            .Where(s => s.ClientId == clientId)
                            .Include(s => s.Employee)
                            .Include(s => s.Client)
                            .OrderByDescending(s => s.CreatedAt)
                            .ToListAsync();
        }

        public bool CheckEmployeeExisById(int empId, int clientId)
        {
            var emp = _appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId  == empId && e.ClientId == clientId);
            return emp != null;
        }

        public bool CheckEmployeeExisByCode(string empCode, int clientId)
        {
            var emp = _appDbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeCode == empCode && e.ClientId == clientId);
            return emp != null;
        }

        public bool SalaryDisbursementByBatch(Employee emp, int clientId)
        {
            Employee e = _appDbContext.Employees.FirstOrDefault(em => em.EmployeeCode == emp.EmployeeCode && em.ClientId == clientId);
            SalaryDisbursement salaryDisbursement = new SalaryDisbursement
            {
                EmployeeId = e.EmployeeId,
                Amount = emp.Salary,
                ClientId = clientId
            };

            _appDbContext.SalaryDisbursements.AddAsync(salaryDisbursement);
            _appDbContext.SaveChangesAsync();

            return true;
        }

    }
}
