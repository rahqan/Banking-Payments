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
                var client = _httpClientFactory.CreateClient();

                var content = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("secret", _settings.SecretKey),
            new KeyValuePair<string, string>("response", token)
        });

                var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                var responseString = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("Full reCAPTCHA response: {Response}", responseString);

                var reCaptchaResponse = System.Text.Json.JsonSerializer.Deserialize<ReCaptchaResponse>(responseString, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

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
