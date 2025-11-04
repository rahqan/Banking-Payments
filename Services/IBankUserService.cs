//using Banking_Payments.Models.DTO;

//namespace Banking_Payments.Services
//{
//    public interface IBankUserService
//    {
//        Task<ClientDTO> CreateClientAsync(ClientCreationDTO clientCreationDTO);
//        Task<IEnumerable<ClientDTO>> GetAllClientsAsync(int bankId);
//        Task<ClientDTO?> GetClientByIdAsync(int clientId, int bankId);
//        Task<ClientDTO> UpdateClientAsync(int clientId, ClientDTO clientDTO, int bankId);
//        Task<bool> DeleteClientAsync(int clientId, int bankId);
//        Task<ClientDTO> VerifyClientAsync(int clientId, int verifiedBy, int bankId, string verificationStatus, string? notes);
//        Task<IEnumerable<ClientDTO>> GetClientsByVerificationStatusAsync(string status, int bankId);
//    }
//}


using Banking_Payments.Models.DTO;

namespace Banking_Payments.Services
{
    public interface IBankUserService
    {
        Task<ClientDTO> CreateClientAsync(ClientCreationDTO clientCreationDTO, int bankId, int bankUserId);
        Task<IEnumerable<ClientDTO>> GetAllClientsAsync(int bankId);
        Task<ClientDTO?> GetClientByIdAsync(int clientId, int bankId);
        Task<ClientDTO> UpdateClientAsync(int clientId, ClientDTO clientDTO, int bankId);
        Task<bool> DeleteClientAsync(int clientId, int bankId);
        Task<ClientDTO> VerifyClientAsync(int clientId, int verifiedBy, int bankId, string verificationStatus, string? notes);
        Task<IEnumerable<ClientDTO>> GetClientsByVerificationStatusAsync(string status, int bankId);
    }
}