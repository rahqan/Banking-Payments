namespace Banking_Payments.Services
{
    public interface IReCaptchaService
    {
        Task<bool> VerifyTokenAsync(string token);
    }
}
