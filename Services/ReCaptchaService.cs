using Banking_Payments.Models;
using Banking_Payments.Models.DTO;
using Microsoft.Extensions.Options;

namespace Banking_Payments.Services
{
    public class ReCaptchaService:IReCaptchaService
    {
        public readonly ReCaptchaSettings _settings;
        private readonly ILogger<ReCaptchaService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public ReCaptchaService(
           IHttpClientFactory httpClientFactory,
           IOptions<ReCaptchaSettings> settings,
           ILogger<ReCaptchaService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _settings = settings.Value;
            _logger = logger;

        }

        public async Task<bool> VerifyTokenAsync(string token)
        {
            try
            {
                _logger.LogInformation("Using reCAPTCHA secret key: {SecretKey}", _settings.SecretKey);

                var client = _httpClientFactory.CreateClient();
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_settings.SecretKey}&response={token}";

                _logger.LogInformation("Calling reCAPTCHA API: {Url}", url);

                var response = await client.GetStringAsync(url);
                var reCaptchaResponse = System.Text.Json.JsonSerializer.Deserialize<ReCaptchaResponse>(response);

                _logger.LogInformation("ReCAPTCHA verification response: {Response}",
                    System.Text.Json.JsonSerializer.Serialize(reCaptchaResponse));

                return reCaptchaResponse?.Success ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying reCAPTCHA token");
                return false;
            }
        }
    }
}
