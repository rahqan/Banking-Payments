

//using Banking_Payments.Models.DTO;
//using Banking_Payments.Models;
//using Banking_Payments.Repositories;
//using Banking_Payments.Models.Enums;
//using Microsoft.AspNetCore.Mvc;

//namespace Banking_Payments.Services
//{
//    public class BankUserService:IBankUserService
//    {
//        private readonly IClientService _clientService;
//        private readonly IBankUserRepository _bankUserRepository;
//        private readonly ILogger<BankUserService> _logger;

//        public BankUserService(
//            IClientService clientService,
//            IBankUserRepository bankUserRepository,
//            ILogger<BankUserService> logger)
//        {
//            _clientService = clientService;
//            _bankUserRepository = bankUserRepository;
//            _logger = logger;
//        }



//      public async Task<ClientDTO> CreateClientAsync(ClientCreationDTO clientCreationDTO)
//        {
//            if (clientCreationDTO == null)
//                throw new ArgumentNullException(nameof(clientCreationDTO), "Client data cannot be null");

//            if (clientCreationDTO.BankId <= 0)
//                throw new ArgumentException("Valid Bank ID is required");

//            if (clientCreationDTO.BankUserId <= 0)
//                throw new ArgumentException("Valid Bank User ID is required");

//            // Generate unique client code
//            var clientCode = await GenerateClientCodeAsync(clientCreationDTO.BankId);

//            // Check if code already exists (unlikely but safety check)
//            //var existingClient = await _clientService.GetClientByCodeAsync(clientCode);
//            //if (existingClient != null)
//            //    throw new InvalidOperationException("Generated client code already exists");

//            var clientModel = new Client
//            {
//                ClientCode = clientCode,
//                ClientName = clientCreationDTO.ClientName,
//                ClientEmail = clientCreationDTO.ClientEmail,
//                BankId = clientCreationDTO.BankId,
//                ClientAddress = clientCreationDTO.Address ?? string.Empty,
//                ClientBusinessType = clientCreationDTO.ClientBusinessType ?? string.Empty,
//                ClientVerificationStatus = VerificationStatus.Pending,
//                IsActive = true,
//                CreatedAt = DateTime.UtcNow,
//                BankUserId = clientCreationDTO.BankUserId
//            };

//            var createdClient = await _bankUserRepository.CreateClientAsync(clientModel);

//            var clientDTO = await MapToClientDTOAsync(createdClient);

//            _logger.LogInformation("Client created successfully with Code: {ClientCode}", clientCode);
//            return clientDTO;

//        }



//        public async Task<IEnumerable<ClientDTO>> GetAllClients(int bankId) {


//            var clients = await _bankUserRepository.GetAllClientsAsync(bankId);

//            var clientDTOs = new List<ClientDTO>();
//            foreach (var client in clients)
//            {
//                clientDTOs.Add(await MapToClientDTOAsync(client));
//            }

//            return clientDTOs;



//        }


//        public  async Task<ClientDTO> GetClientById(int clientId) {

//            var client = await _bankUserRepository.GetClientByIdAsync(clientId);
//            if (client == null)
//                return null;

//            return await MapToClientDTOAsync(client);



//        }



//        public async Task<Client> UpdateClient(ClientDTO clientDTO) {


//            var existingClient = await _bankUserRepository.GetClientByIdAsync((int)clientDTO.ClientId);
//            if (existingClient == null)
//                throw new KeyNotFoundException("Client not found");

//            // Update only allowed fields
//            existingClient.Name = clientDTO.ClientName;
//            existingClient.Email = clientDTO.ClientEmail;
//            existingClient.BusinessType = clientDTO.ClientBusinessType ?? string.Empty;
//            existingClient.Address = clientDTO.ClientAddress ?? string.Empty;
//            // Don't update: Code, BankId, VerificationStatus, CreatedAt, BankUserId, IsActive

//            var result = await _bankUserRepository.UpdateClient(existingClient);

//            if (!result)
//                throw new InvalidOperationException("Failed to update client");

//            _logger.LogInformation("Client updated successfully: {ClientId}", clientDTO.ClientId);
//            return await MapToClientDTOAsync(existingClient);



//        }



//        public async Task<bool> DeleteClient(int clientId) {

//            var existingClient = await _bankUserRepository.GetClientByIdAsync(clientId);
//            if (existingClient == null)
//                throw new KeyNotFoundException("Client not found");

//            var result = await _bankUserRepository.DeleteClientAsync(existingClient);

//            if (result)
//                _logger.LogInformation("Client deleted successfully: {ClientId}", clientId);

//            return result;

//        }


//        public async Task<ClientDTO> VerifyClient(int clientId,ClientDTO clientDTO) {

//            var client = await _bankUserRepository.GetClientByIdAsync(clientId);
//            if (client == null)
//                throw new KeyNotFoundException("Client not found");

//         //   if (client.BankId != bankId)
//           //     throw new UnauthorizedAccessException("You can only verify clients from your own bank");

//            //var oldStatus = client.VerficicationStatus;

//           // if (!IsValidTransition(oldStatus, verificationStatus))
//             //   throw new InvalidOperationException($"Invalid status transition from {oldStatus} to {verificationStatus}");

//            client.VerficicationStatus = verificationStatus;
//            client.BankUserId = verifiedBy; // Storing who verified

//            var result = await _clientRepository.UpdateClient(client);

//            if (!result)
//                throw new InvalidOperationException("Failed to update client verification status");

//            var clientDTO = await MapToClientDTOAsync(client);
//            _logger.LogInformation("Client verification status updated: Client ID: {ClientId}, From: {OldStatus}, To: {NewStatus}",
//                clientId, oldStatus, verificationStatus);

//            return clientDTO;


//        }






//        public async Task<IEnumerable<ClientDTO>> GetClientsByVerificationStatus(string status) {


//            // Parse string to enum
//            if (!Enum.TryParse<VerificationStatus>(status, true, out var verificationStatus))
//                throw new ArgumentException($"Invalid verification status: {status}");

//            var clients = await _bankUserRepository.GetClientsByVerificationStatusAsync(verificationStatus);

//            var clientDTOs = new List<ClientDTO>();
//            foreach (var client in clients)
//            {
//                clientDTOs.Add(await MapToClientDTOAsync(client));
//            }

//            return clientDTOs;

//        }





















//        // Helper Methods

//        private async Task<string> GenerateClientCodeAsync(int bankId)
//        {
//            var bank = await _bankUserRepository.GetBankByIdAsync(bankId);
//            var bankCode = bank?.Code ?? "BNK";

//            var clientCount = await _clientService.GetClientCountByBankIdAsync(bankId);
//            var sequenceNumber = (clientCount + 1).ToString("D4"); // 4-digit sequence

//            return $"{bankCode}-CLT-{sequenceNumber}";
//        }

//        private bool IsValidTransition(VerificationStatus currentStatus, VerificationStatus newStatus)
//        {
//            // Define valid transitions
//            if (currentStatus == VerificationStatus.Pending)
//                return newStatus == VerificationStatus.Verified || newStatus == VerificationStatus.Rejected;

//            if (currentStatus == VerificationStatus.Rejected)
//                return newStatus == VerificationStatus.Pending || newStatus == VerificationStatus.Verified;

//            if (currentStatus == VerificationStatus.Verified)
//                return newStatus == VerificationStatus.Rejected; // Can reject a verified client

//            return false;
//        }

//        private async Task<ClientDTO> MapToClientDTOAsync(Client client)
//        {
//            // Get counts for related entities
//            int totalEmployees = 0;
//            int totalBeneficiaries = 0;
//            int totalPayments = 0;

//            // If collections are loaded, use them
//            if (client.Employees != null && client.Beneficiaries != null && client.Payments != null)
//            {
//                totalEmployees = client.Employees.Count;
//                totalBeneficiaries = client.Beneficiaries.Count;
//                totalPayments = client.Payments.Count;
//            }
//            else
//            {
//                // Otherwise fetch counts from repository
//                var counts = await _clientService.GetClientCountsAsync(client.ClientId);
//                totalEmployees = counts.EmployeeCount;
//                totalBeneficiaries = counts.BeneficiaryCount;
//                totalPayments = counts.PaymentCount;
//            }

//            return new ClientDTO
//            {
//                ClientId = client.ClientId,
//                ClientCode = client.ClientCode,
//                ClientName = client.ClientName,

//                ClientEmail = client.ClientEmail,
//                ClientBusinessType = client.ClientBusinessType,
//                ClientAddress = client.ClientAddress,
//                ClientVerificationStatus = client.ClientVerificationStatus,
//                BankId = client.BankId,
//                BankUserId = client.BankUserId,
//                VerifiedBy = client.BankUserId, // Using BankUserId as VerifiedBy
//                VerifiedAt = client.ClientVerificationStatus == VerificationStatus.Verified ? client.CreatedAt : null,
//                TotalEmployees = totalEmployees,
//                TotalBeneficiaries = totalBeneficiaries,
//                TotalPayments = totalPayments
//            };
//        }


//    }
//}


using Banking_Payments.Models.DTO;
using Banking_Payments.Models;
using Banking_Payments.Repositories;
using Banking_Payments.Models.Enums;

namespace Banking_Payments.Services
{
    public class BankUserService : IBankUserService
    {
        private readonly IBankUserRepository _bankUserRepository;
        private readonly ILogger<BankUserService> _logger;

        public BankUserService(
            IBankUserRepository bankUserRepository,
            ILogger<BankUserService> logger)
        {
            _bankUserRepository = bankUserRepository;
            _logger = logger;
        }

        public async Task<ClientDTO> CreateClientAsync(ClientCreationDTO clientCreationDTO)
        {
            if (clientCreationDTO == null)
                throw new ArgumentNullException(nameof(clientCreationDTO), "Client data cannot be null");

            if (clientCreationDTO.BankId <= 0)
                throw new ArgumentException("Valid Bank ID is required");

            if (clientCreationDTO.BankUserId <= 0)
                throw new ArgumentException("Valid Bank User ID is required");

            // Generate unique client code
            var clientCode = await GenerateClientCodeAsync(clientCreationDTO.BankId);

            // Check if code already exists (unlikely but safety check)
            var existingClient = await _bankUserRepository.GetClientByCodeAsync(clientCode);
            if (existingClient != null)
                throw new InvalidOperationException("Generated client code already exists");

            var clientModel = new Client
            {
                ClientCode = clientCode,
                ClientName = clientCreationDTO.ClientName,
                ClientEmail = clientCreationDTO.ClientEmail,
                BankId = clientCreationDTO.BankId,
                ClientAddress = clientCreationDTO.Address ?? string.Empty,
                ClientBusinessType = clientCreationDTO.ClientBusinessType ?? string.Empty,
                ClientVerificationStatus = VerificationStatus.Pending,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                BankUserId = clientCreationDTO.BankUserId
            };

            var createdClient = await _bankUserRepository.CreateClientAsync(clientModel);

            var clientDTO = MapToClientDTO(createdClient);

            _logger.LogInformation("Client created successfully with Code: {ClientCode}", clientCode);
            return clientDTO;
        }

        public async Task<IEnumerable<ClientDTO>> GetAllClientsAsync(int bankId)
        {
            var clients = await _bankUserRepository.GetAllClientsAsync(bankId);

            var clientDTOs = clients.Select(client => MapToClientDTO(client)).ToList();

            return clientDTOs;
        }

        public async Task<ClientDTO?> GetClientByIdAsync(int clientId)
        {
            var client = await _bankUserRepository.GetClientByIdAsync(clientId);
            if (client == null)
                return null;

            return MapToClientDTO(client);
        }

        public async Task<ClientDTO> UpdateClientAsync(int clientId, ClientDTO clientDTO)
        {
            var existingClient = await _bankUserRepository.GetClientByIdAsync(clientId);
            if (existingClient == null)
                throw new KeyNotFoundException("Client not found");

            // Update only allowed fields
            existingClient.ClientName = clientDTO.ClientName;
            existingClient.ClientEmail = clientDTO.ClientEmail;
            existingClient.ClientBusinessType = clientDTO.ClientBusinessType ?? string.Empty;
            existingClient.ClientAddress = clientDTO.ClientAddress ?? string.Empty;
            // Don't update: Code, BankId, VerificationStatus, CreatedAt, BankUserId, IsActive

            var result = await _bankUserRepository.UpdateClientAsync(existingClient);

            if (!result)
                throw new InvalidOperationException("Failed to update client");

            _logger.LogInformation("Client updated successfully: {ClientId}", clientDTO.ClientId);
            return MapToClientDTO(existingClient);
        }

        public async Task<bool> DeleteClientAsync(int clientId)
        {
            var existingClient = await _bankUserRepository.GetClientByIdAsync(clientId);
            if (existingClient == null)
                throw new KeyNotFoundException("Client not found");

            var result = await _bankUserRepository.DeleteClientAsync(existingClient);

            if (result)
                _logger.LogInformation("Client deleted successfully: {ClientId}", clientId);

            return result;
        }

        public async Task<ClientDTO> VerifyClientAsync(
            int clientId,
            int verifiedBy,
            int bankId,
            VerificationStatus verificationStatus,
            string? notes)
        {
            var client = await _bankUserRepository.GetClientByIdAsync(clientId);
            if (client == null)
                throw new KeyNotFoundException("Client not found");

            if (client.BankId != bankId)
                throw new UnauthorizedAccessException("You can only verify clients from your own bank");

            var oldStatus = client.ClientVerificationStatus;

            if (!IsValidTransition(oldStatus, verificationStatus))
                throw new InvalidOperationException($"Invalid status transition from {oldStatus} to {verificationStatus}");

            client.ClientVerificationStatus = verificationStatus;
            client.BankUserId = verifiedBy; // Storing who verified

            var result = await _bankUserRepository.UpdateClientAsync(client);

            if (!result)
                throw new InvalidOperationException("Failed to update client verification status");

            var clientDTO = MapToClientDTO(client);
            _logger.LogInformation("Client verification status updated: Client ID: {ClientId}, From: {OldStatus}, To: {NewStatus}",
                clientId, oldStatus, verificationStatus);

            return clientDTO;
        }

        public async Task<IEnumerable<ClientDTO>> GetClientsByVerificationStatusAsync(string status)
        {
            // Parse string to enum
            if (!Enum.TryParse<VerificationStatus>(status, true, out var verificationStatus))
                throw new ArgumentException($"Invalid verification status: {status}");

            var clients = await _bankUserRepository.GetClientsByVerificationStatusAsync(verificationStatus);

            var clientDTOs = clients.Select(client => MapToClientDTO(client)).ToList();

            return clientDTOs;
        }

        // Helper Methods

        private async Task<string> GenerateClientCodeAsync(int bankId)
        {
            var bank = await _bankUserRepository.GetBankByIdAsync(bankId);
            var bankCode = bank?.Code ?? "BNK";

            var clientCount = await _bankUserRepository.GetClientCountByBankIdAsync(bankId);
            var sequenceNumber = (clientCount + 1).ToString("D4"); // 4-digit sequence

            return $"{bankCode}-CLT-{sequenceNumber}";
        }

        private bool IsValidTransition(VerificationStatus currentStatus, VerificationStatus newStatus)
        {
            // Define valid transitions
            if (currentStatus == VerificationStatus.Pending)
                return newStatus == VerificationStatus.Verified || newStatus == VerificationStatus.Rejected;

            if (currentStatus == VerificationStatus.Rejected)
                return newStatus == VerificationStatus.Pending || newStatus == VerificationStatus.Verified;

            if (currentStatus == VerificationStatus.Verified)
                return newStatus == VerificationStatus.Rejected; // Can reject a verified client

            return false;
        }

        private ClientDTO MapToClientDTO(Client client)
        {
            // Get counts for related entities
            int totalEmployees = 0;
            int totalBeneficiaries = 0;
            int totalPayments = 0;

            // If collections are loaded, use them
            if (client.Employees != null)
                totalEmployees = client.Employees.Count;

            if (client.Beneficiaries != null)
                totalBeneficiaries = client.Beneficiaries.Count;

            if (client.Payments != null)
                totalPayments = client.Payments.Count;

            return new ClientDTO
            {
                ClientId = client.ClientId,
                ClientCode = client.ClientCode,
                ClientName = client.ClientName,
                ClientEmail = client.ClientEmail,
                ClientBusinessType = client.ClientBusinessType,
                ClientAddress = client.ClientAddress,
                ClientVerificationStatus = client.ClientVerificationStatus,
                BankId = client.BankId,
                BankUserId = client.BankUserId,
                VerifiedBy = client.BankUserId, // Using BankUserId as VerifiedBy
                VerifiedAt = client.ClientVerificationStatus == VerificationStatus.Verified ? client.CreatedAt : null,
                TotalEmployees = totalEmployees,
                TotalBeneficiaries = totalBeneficiaries,
                TotalPayments = totalPayments
            };
        }
    }
}