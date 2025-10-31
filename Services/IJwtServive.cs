namespace dummy_api.Services
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string role, int? bankId = null);
    }
}
