using System.Threading.Tasks;

namespace Banking_Payments.Services
{
    public interface IEmailService
    {
        Task SendClientApprovalEmailAsync(string recipientEmail, string recipientName, string bankName);
        Task SendClientRejectionEmailAsync(string recipientEmail, string recipientName, string bankName, string? notes);
    }
}
