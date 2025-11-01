using Microsoft.AspNetCore.Mvc;
using Banking_Payments.Services;
using Banking_Payments.Models;

namespace Banking_Payments.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ClientController : ControllerBase
    {
        public readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet("get-all-client-by-id")]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClientByBankIdAsync(int id)
        {
            var clients = await clientService.GetAllClientByBankIdAsync(id);
            return Ok(clients);
        }

        [HttpGet("get-clients-all")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsAllAsync(int id)
        {
            var clients = await clientService.GetClientsAllAsync(id);
            return Ok(clients);
        }

        [HttpGet("get-client-by-registration")]
        public async Task<ActionResult<Client>> GetClientByRegisterationNumberAsync(string registerationNumber, int bankId)
        {
            var client = await clientService.GetClientByRegisterationNumberAsync(registerationNumber, bankId);
            return Ok(client);
        }

        [HttpGet("get-client-with-status")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsWithStatusAsync(string status, int bankId)
        {
            var clients = await clientService.GetClientsWithStatusAsync(status, bankId);
            return Ok(clients);
        }

        [HttpPost("add-beneficiary")]
        public async Task<ActionResult<Beneficiary>> AddBeneficiaryAsync(Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                Beneficiary res = await clientService.AddBeneficiaryAsync(beneficiary);
                return CreatedAtAction("AddBeneficiary", res);
            }
            return BadRequest();
        }

        [HttpPut("update-beneficiary")]
        public async Task<ActionResult<Beneficiary>> UpdateBeneficiaryAsync(Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                var res = await clientService.UpdateBeneficiaryAsync(beneficiary);
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpDelete("delete-beneficiary")]
        public async Task<ActionResult<bool>> DeleteBeneficiaryAsync(int id)
        {
            var res = await clientService.DeleteBeneficiaryAsync(id);
            if (!res) return NotFound("Invalid id");
            return Ok("Beneficiary deleted successfully");
        }

        [HttpGet("get-beneficiary-by-id/{id}")]
        public async Task<ActionResult<Beneficiary>> ShowBeneficiaryByIdAsync(int id)
        {
            var res = await clientService.ShowBeneficiaryByIdAsync(id);
            if (res == null)
            {
                return NotFound("Invalid id");
            }
            return Ok(res);
        }

        [HttpGet("get-all-beneficiaries/{clientId}")]
        public async Task<ActionResult<IEnumerable<Beneficiary>>> ShowBeneficiariesAsync(int clientId)
        {
            IEnumerable<Beneficiary> res = await clientService.ShowBeneficiariesAsync(clientId);
            return Ok(res);
        }

        [HttpPost("add-payment")]
        public async Task<ActionResult<Payment>> AddPaymentAsync(Payment payment)
        {
            if (ModelState.IsValid)
            {
                Payment res = await clientService.AddPaymentAsync(payment);
                return CreatedAtAction("AddPayment", res);
            }
            return BadRequest();
        }

        [HttpGet("get-payment-by-id/{id}")]
        public async Task<ActionResult<Payment>> ShowPaymentByIdAsync(int id)
        {
            var res = await clientService.ShowPaymentByIdAsync(id);
            if (res == null)
            {
                return NotFound("Invalid id");
            }
            return Ok(res);
        }

        [HttpGet("get-all-payment/{clientId}")]
        public async Task<ActionResult<IEnumerable<Payment>>> ShowAllPaymentsAsync(int clientId)
        {
            var res = await clientService.ShowAllPaymentsAsync(clientId);
            return Ok(res);
        }

        [HttpPost("add-employee")]
        public async Task<ActionResult<Employee>> AddEmployeeAsync(Employee emp)
        {
            if (ModelState.IsValid)
            {
                var res = await clientService.AddEmployeeAsync(emp);
                return CreatedAtAction("AddEmployeeAsync", res);
            }
            return BadRequest();
        }

        [HttpGet("get-employee-by-id/{id}")]
        public async Task<ActionResult<Employee>> ShowEmployeeByIdAsync(int id)
        {
            var res = await clientService.ShowEmployeeByIdAsync(id);
            if (res == null)
            {
                return NotFound("Invalid id");
            }
            return Ok(res);
        }

        [HttpGet("get-all-employee/{clientId}")]
        public async Task<ActionResult<IEnumerable<Employee>>> ShowAllEmployeeAsync(int clientId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var res = await clientService.ShowAllEmployeeAsync(clientId, pageNumber, pageSize);
            return Ok(res);
        }

        [HttpGet("get-salary-disbursement-by-employee-id/{empId}")]
        public async Task<ActionResult<IEnumerable<SalaryDisbursement>>> ShowSalaryDisbursementByEmpIdAsync(int empId)
        {
            var res = await clientService.ShowSalaryDisbursementByEmpIdAsync(empId);
            return Ok(res);
        }

        [HttpGet("get-salary-disbursement-by-client-id/{clientId}")]
        public async Task<ActionResult<IEnumerable<SalaryDisbursement>>> ShowSalaryDisbursementByClientIdAsync(int clientId)
        {
            var res = await clientService.ShowSalaryDisbursementByClientIdAsync(clientId);
            return Ok(res);
        }

        [HttpPost("salary-disbursement-by-batch/{clientId}")]
        public async Task<ActionResult> SalaryDisbursement(IFormFile file, int clientId, [FromQuery] int batchSize = 10)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Please upload a valid Excel file");
                }

                if (!file.FileName.EndsWith(".xlsx") && !file.FileName.EndsWith(".xls"))
                {
                    return BadRequest("Only Excel files are supported");
                }

                List<Employee> employees = new List<Employee>();
                using (var stream = file.OpenReadStream())
                {
                    employees = await clientService.ReadEmployeesFromExcel(stream);
                }

                if (employees.Count == 0)
                {
                    return BadRequest("No employee records found in the file");
                }

                var res = clientService.SalaryDisbursementByBatch(employees, batchSize, clientId);

                return Ok(res);
            }
            catch
            {
                throw new Exception("Something went wrong in controller at 80");
            }
        }
    }
}
