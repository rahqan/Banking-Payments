//using Banking_Payments.Models.DTO;
//using Microsoft.AspNetCore.Mvc;

//namespace Banking_Payments.Services
//{
//    public interface IBankUserService
//    {

//        public Task<ClientDTO> CreateClientAsync(ClientCreationDTO clientCreationDTO);
//        public  Task<IEnumerable<ClientDTO>> GetAllClients(int bankId);


//        public Task<ClientDTO> GetClientById(int clientId);

//        public Task<ClientDTO> UpdateClient(int clientId, ClientDTO clientDTO)
//            ;

//        public  Task<bool> DeleteClient(int clientId)


//;

//        public  Task<ClientDTO> VerifyClient(int clientId);


//        public Task<IEnumerable<ClientDTO>> GetClientsByVerificationStatus(string status)


//            ;








//    }
//}
using Banking_Payments.Models.DTO;
using Banking_Payments.Models.Enums;

namespace Banking_Payments.Services
{
    public interface IBankUserService
    {
        Task<ClientDTO> CreateClientAsync(ClientCreationDTO clientCreationDTO);
        Task<IEnumerable<ClientDTO>> GetAllClientsAsync(int bankId);
        Task<ClientDTO?> GetClientByIdAsync(int clientId);
        Task<ClientDTO> UpdateClientAsync(int clientId, ClientDTO clientDTO);
        Task<bool> DeleteClientAsync(int clientId);
        Task<ClientDTO> VerifyClientAsync(int clientId, int verifiedBy, int bankId, VerificationStatus verificationStatus, string? notes);
        Task<IEnumerable<ClientDTO>> GetClientsByVerificationStatusAsync(string status);
    }
}