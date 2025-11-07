using Microsoft.AspNetCore.Mvc;
using Banking_Payments.Services;
using Banking_Payments.Models;
using Banking_Payments.Models.DTO;

namespace Banking_Payments.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ClientController : ControllerBase
    {
        public readonly IClientService clientService;
        private readonly ILogger<ClientController> logger;
        public ClientController(IClientService clientService, ILogger<ClientController> logger)
        {
            this.clientService = clientService;
            this.logger = logger;
        }

        [HttpDelete("delete-employee-by-id/{employeeId}")]
        public async Task<ActionResult> DeleteEmployeeByEmployeeId(int employeeId)
        {
            var res = await clientService.DeleteEmployeeByEmployeeId(employeeId);
            return Ok();
        }

        [HttpGet("get-all-client-by-id")]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClientByBankIdAsync(int id)
        {
            var clients = await clientService.GetAllClientByBankIdAsync(id);
            return Ok(clients);
        }

        [HttpGet("get-client-for-bank/{clientId}")]
        public async Task<ActionResult<ClientBankDetailsDTO>> GetClientForBankDetailsAsync(int clientId)
        {
            ClientBankDetailsDTO res = await clientService.GetClientForBankDetailsAsync(clientId);
            if (res == null)
            {
                return BadRequest("Invalid id");
            }
            return Ok(res);
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
                Console.WriteLine("Inside if condition ::: ");
                Payment res = await clientService.AddPaymentAsync(payment);
                return CreatedAtAction("AddPayment", res);
            }
            Console.WriteLine("Inside controller ::: ");
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

        [HttpPost("individual-salary-disbursement")]
        public async Task<ActionResult<SalaryDisbursement>> AddSalaryDisbursementIndividuallyAsync(SalaryDisbursement s)
        {

            if (ModelState.IsValid)
            {
                var res = await clientService.AddSalaryDisbursementIndividuallyAsync(s);
                if (res == null)
                {
                    return Conflict("Salary already given this month.");
                }
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpPost("salary-disbursement-by-batch")]
        public async Task<IActionResult> SalaryDisbursement([FromForm] SalaryDisbursementForm form)
        {
            try
            {
                logger.LogInformation("Starting salary disbursement process for ClientId: {ClientId}", form.ClientId);

                if (form.File == null || form.File.Length == 0)
                {
                    return BadRequest("No file uploaded");
                }

                // Validate file extension
                var fileExtension = Path.GetExtension(form.File.FileName).ToLower();
                if (fileExtension != ".xlsx" && fileExtension != ".xls" && fileExtension != ".csv")
                {
                    return BadRequest("Invalid file format. Please upload an Excel file (.xlsx or .xls)");
                }

                logger.LogInformation("Received file: {FileName}, Size: {FileSize} bytes", form.File.FileName, form.File.Length);

                // Validate batch size
                if (form.BatchSize <= 0)
                {
                    form.BatchSize = 10;
                }

                // Copy the stream to a MemoryStream first
                List<Employee> employees;
                using (var memoryStream = new MemoryStream())
                {
                    await form.File.CopyToAsync(memoryStream);
                    memoryStream.Position = 0; // Reset position to beginning

                    employees = await clientService.ReadEmployeesFromExcel(memoryStream);

                    foreach (var emp in employees)
                    {
                        logger.LogInformation("Employee read from file: {EmployeeCode}, Name: {Name}, Salary: {Salary}",
                            emp.EmployeeCode, emp.Name, emp.Salary);
                    }
                }

                if (employees == null || !employees.Any())
                {
                    return BadRequest("No valid employees found in the file");
                }

                logger.LogInformation("Number of employees read from file: {Count}", employees.Count);

                // FIXED: Removed 'await' since method is synchronous
                var result = clientService.SalaryDisbursementByBatch(employees, form.BatchSize, form.ClientId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during salary disbursement");
                return StatusCode(500, new
                {
                    message = "Something went wrong",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }
    }
}