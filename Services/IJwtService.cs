namespace Banking_Payments.Services
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string role, int? bankId = null);
    }
}
