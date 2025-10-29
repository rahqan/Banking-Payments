using Banking_Payments.Models;
using Banking_Payments.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("add-beneficiary")]
        public async Task<ActionResult<Beneficiary>> AddBeneficiary(Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                Beneficiary res = await clientService.AddBeneficiary(beneficiary);
                return CreatedAtAction("AddBeneficiary", res); 
            }
            return BadRequest();
            
        }

        [HttpGet("all-beneficiaries")]
        public async Task<ActionResult<IEnumerable<Beneficiary>>> ShowBeneficiaries()
        {
            IEnumerable<Beneficiary> res = await clientService.ShowBeneficiaries();
            return Ok(res);
        }

        [HttpPost("add-payment")]
        public async Task<ActionResult<Payment>> AddPayment(Payment payment)
        {
            if (ModelState.IsValid)
            {
                Payment res = await clientService.AddPayment(payment);
                return CreatedAtAction("AddPayment", res);
            }
            return BadRequest();
        }


    }
}
