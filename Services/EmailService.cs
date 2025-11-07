using Banking_Payments.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Banking_Payments.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> settings, ILogger<EmailService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task SendClientApprovalEmailAsync(string recipientEmail, string recipientName, string bankName)
        {
            Console.WriteLine("Preparing to send approval email...");
            Console.WriteLine($"Recipient: {recipientEmail}, Name: {recipientName}, Bank: {bankName}");
            var subject = "Your Account Has Been Approved";
            var body = $@"
                <p>Dear {recipientName},</p>
                <p>Your client account with <strong>{bankName}</strong> has been approved successfully.</p>
                <p>You can now access our services. Please log in to your account for more details.</p>
                <p>Best regards,<br>{bankName} Team</p>";

            await SendEmailAsync(recipientEmail, subject, body);
            Console.WriteLine("Approval email sent.");
        }

        public async Task SendClientRejectionEmailAsync(string recipientEmail, string recipientName, string bankName, string? notes)
        {
            var subject = "Your Account Application Has Been Rejected";
            var reason = string.IsNullOrWhiteSpace(notes) ? "no specific reason provided" : notes;

            var body = $@"
                <p>Dear {recipientName},</p>
                <p>We regret to inform you that your client account application with <strong>{bankName}</strong> has been rejected.</p>
                <p>Reason: {reason}</p>
                <p>If you believe this was a mistake or wish to reapply, please contact our support team.</p>
                <p>Best regards,<br>{bankName} Team</p>";

            await SendEmailAsync(recipientEmail, subject, body);
        }

        private async Task SendEmailAsync(string to, string subject, string htmlBody)
        {
            using var message = new MailMessage
            {
                From = new MailAddress(_settings.FromEmail, _settings.FromName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            message.To.Add(to);

            using var smtp = new SmtpClient(_settings.SmtpHost)  // ✅ Correct
            {
                Port = _settings.SmtpPort,
                Credentials = new NetworkCredential(
        _settings.Username,  // ✅ Correct
        _settings.Password   // ✅ Correct
    ),
                EnableSsl = _settings.EnableSsl,
                UseDefaultCredentials = false
            };

            try
            {
                await smtp.SendMailAsync(message);
                _logger.LogInformation("Email sent successfully to {Email}", to);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to send email to {Email}", to);
            }
        }
    }
}
