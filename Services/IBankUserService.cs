using Banking_Payments.Models.DTO;
using Banking_Payments.Models.Enums;

namespace Banking_Payments.Services
{
    public interface IBankUserService
    {
        Task<ClientDTO> CreateClientAsync(ClientCreationDTO clientCreationDTO);
        Task<IEnumerable<ClientDTO>> GetAllClientsAsync(int bankId);
        Task<ClientDTO?> GetClientByIdAsync(int clientId, int bankId);
        Task<ClientDTO> UpdateClientAsync(int clientId, ClientDTO clientDTO, int bankId);
        Task<bool> DeleteClientAsync(int clientId, int bankId);
        Task<ClientDTO> VerifyClientAsync(int clientId, int verifiedBy, int bankId, VerificationStatus verificationStatus, string? notes);
        Task<IEnumerable<ClientDTO>> GetClientsByVerificationStatusAsync(string status, int bankId);
    }
}