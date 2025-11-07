using Microsoft.AspNetCore.Mvc;

namespace Banking_Payments.Models
{
    public class SalaryDisbursementForm
    {
        [FromForm(Name = "file")]
        public IFormFile File { get; set; }

        [FromForm(Name = "clientId")]
        public int ClientId { get; set; }

        [FromForm(Name = "batchSize")]
        public int BatchSize { get; set; } = 10;
    }

}
