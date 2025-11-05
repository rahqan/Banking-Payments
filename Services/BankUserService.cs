using Banking_Payments.Models;
using Banking_Payments.Models.DTO;
using Banking_Payments.Models.Enums;
using Banking_Payments.Repositories;

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

        public async Task<ClientDTO> CreateClientAsync(Client client)
        {
            //if (clientCreationDTO == null)
            //    throw new ArgumentNullException(nameof(clientCreationDTO), "Client data cannot be null");

            //if (clientCreationDTO.BankId <= 0)
            //    throw new ArgumentException("Valid Bank ID is required");

            //if (clientCreationDTO.BankUserId <= 0)
            //    throw new ArgumentException("Valid Bank User ID is required");

            //// Generate unique client code
            //var clientCode = await GenerateClientCodeAsync(clientCreationDTO.BankId);

            //// Check if code already exists (unlikely but safety check)
            //var existingClient = await _bankUserRepository.GetClientByCodeAsync(clientCode);
            //if (existingClient != null)
            //    throw new InvalidOperationException("Generated client code already exists");

            //var clientModel = new Client
            //{
            //    Code = clientCode,
            //    Name = clientCreationDTO.ClientName,
            //    Email = clientCreationDTO.ClientEmail,
            //    BusinessType = clientCreationDTO.ClientBusinessType,
            //    Address = clientCreationDTO.Address,
            //    Password = clientCreationDTO.ClientPassword,
            //    RegisterationNumber = clientCreationDTO.RegisterationNumber,
            //    VerificationStatus = "Approved",
            //    AccountNumber = clientCreationDTO.AccountNumber,
            //    IfscCode = clientCreationDTO.IfscCode,
            //    Balance = clientCreationDTO.Balance,
            //    CreatedAt = DateTime.UtcNow,
            //    BankId = clientCreationDTO.BankId,
            //    BankUserId = clientCreationDTO.BankUserId
                
                //Code = clientCode,
                //Name = clientCreationDTO.ClientName,
                //Password = clientCreationDTO.ClientPassword,
                //Email = clientCreationDTO.ClientEmail,
                //BankId = clientCreationDTO.BankId,
                //Address = clientCreationDTO.Address ?? string.Empty,
                //BusinessType = clientCreationDTO.ClientBusinessType ?? string.Empty,
                //VerificationStatus = "Pending",
                //IsActive = true,
                //CreatedAt = DateTime.UtcNow,
                //BankUserId = clientCreationDTO.BankUserId
            //};

            var createdClient = await _bankUserRepository.CreateClientAsync(client);

            var clientDTO = MapToClientDTO(createdClient);

            _logger.LogInformation("Client created successfully with Code: {ClientCode}", client.Code);
            return clientDTO;
        }

        public async Task<IEnumerable<ClientDTO>> GetAllClientsAsync(int bankId)
        {
            var clients = await _bankUserRepository.GetAllClientsAsync(bankId);

            var clientDTOs = clients.Select(client => MapToClientDTO(client)).ToList();

            return clientDTOs;
        }

        public async Task<ClientDTO?> GetClientByIdAsync(int clientId, int bankId)
        {
            var client = await _bankUserRepository.GetClientByIdAsync(clientId);
            if (client == null)
                return null;

            // Verify the client belongs to the bank user's bank
            if (client.BankId != bankId)
            {
                _logger.LogWarning("Unauthorized access attempt: BankId {BankId} tried to access Client {ClientId} belonging to BankId {ClientBankId}",
                    bankId, clientId, client.BankId);
                throw new UnauthorizedAccessException("You can only access clients from your own bank");
            }

            return MapToClientDTO(client);
        }

        public async Task<ClientDTO> UpdateClientAsync(int clientId, ClientDTO clientDTO, int bankId)
        {
            var existingClient = await _bankUserRepository.GetClientByIdAsync(clientId);
            if (existingClient == null)
                throw new KeyNotFoundException("Client not found");

            // Verify the client belongs to the bank user's bank
            if (existingClient.BankId != bankId)
            {
                _logger.LogWarning("Unauthorized update attempt: BankId {BankId} tried to update Client {ClientId} belonging to BankId {ClientBankId}",
                    bankId, clientId, existingClient.BankId);
                throw new UnauthorizedAccessException("You can only update clients from your own bank");
            }

            // Update only allowed fields
            existingClient.Name = clientDTO.ClientName;
            existingClient.Email = clientDTO.ClientEmail;
            existingClient.BusinessType = clientDTO.ClientBusinessType ?? string.Empty;
            existingClient.Address = clientDTO.ClientAddress ?? string.Empty;
            // Don't update: Code, BankId, VerificationStatus, CreatedAt, BankUserId, IsActive

            var result = await _bankUserRepository.UpdateClientAsync(existingClient);

            if (!result)
                throw new InvalidOperationException("Failed to update client");

            _logger.LogInformation("Client updated successfully: {ClientId}", clientDTO.ClientId);
            return MapToClientDTO(existingClient);
        }

        public async Task<bool> DeleteClientAsync(int clientId, int bankId)
        {
            var existingClient = await _bankUserRepository.GetClientByIdAsync(clientId);
            if (existingClient == null)
                throw new KeyNotFoundException("Client not found");

            // Verify the client belongs to the bank user's bank
            if (existingClient.BankId != bankId)
            {
                _logger.LogWarning("Unauthorized delete attempt: BankId {BankId} tried to delete Client {ClientId} belonging to BankId {ClientBankId}",
                    bankId, clientId, existingClient.BankId);
                throw new UnauthorizedAccessException("You can only delete clients from your own bank");
            }

            var result = await _bankUserRepository.DeleteClientAsync(existingClient);

            if (result)
                _logger.LogInformation("Client deleted successfully: {ClientId}", clientId);

            return result;
        }

        public async Task<ClientDTO> VerifyClientAsync(
            int clientId,
            int verifiedBy,
            int bankId,
            string verificationStatus,
            string? notes)
        {
            var client = await _bankUserRepository.GetClientByIdAsync(clientId);
            if (client == null)
                throw new KeyNotFoundException("Client not found");

            if (client.BankId != bankId)
            {
                _logger.LogWarning("Unauthorized verification attempt: BankId {BankId} tried to verify Client {ClientId} belonging to BankId {ClientBankId}",
                    bankId, clientId, client.BankId);
                throw new UnauthorizedAccessException("You can only verify clients from your own bank");
            }

            var oldStatus = client.VerificationStatus;

            if (!IsValidTransition(oldStatus, verificationStatus))
                throw new InvalidOperationException($"Invalid status transition from {oldStatus} to {verificationStatus}");

            client.VerificationStatus = verificationStatus;
            client.BankUserId = verifiedBy; // Storing who verified

            var result = await _bankUserRepository.UpdateClientAsync(client);

            if (!result)
                throw new InvalidOperationException("Failed to update client verification status");

            var clientDTO = MapToClientDTO(client);
            _logger.LogInformation("Client verification status updated: Client ID: {ClientId}, From: {OldStatus}, To: {NewStatus}",
                clientId, oldStatus, verificationStatus);

            return clientDTO;
        }

        public async Task<IEnumerable<ClientDTO>> GetClientsByVerificationStatusAsync(string status, int bankId)
        {
            // Parse string to enum
            if (!Enum.TryParse<VerificationStatus>(status, true, out var verificationStatus))
                throw new ArgumentException($"Invalid verification status: {status}");

            var clients = await _bankUserRepository.GetClientsByVerificationStatusAsync(Convert.ToString(verificationStatus), bankId);

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

        private bool IsValidTransition(string currentStatus, string newStatus)
        {
            // Define valid transitions
            if (currentStatus == "Pending")
                return newStatus == "Verified" || newStatus == "Rejected";

            if (currentStatus == "Rejected")
                return newStatus == "Pending" || newStatus == "Verified";

            if (currentStatus == "Verified")
                return newStatus == "Rejected"; // Can reject a verified client

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
                ClientCode = client.Code,
                ClientName = client.Name,
                ClientEmail = client.Email,
                ClientBusinessType = client.BusinessType,
                ClientAddress = client.Address,
                ClientVerificationStatus = client.VerificationStatus,
                BankId = client.BankId,
                BankUserId = client.BankUserId,
                VerifiedBy = client.BankUserId, // Using BankUserId as VerifiedBy
                VerifiedAt = client.VerificationStatus == "Verified" ? client.CreatedAt : null,
                TotalEmployees = totalEmployees,
                TotalBeneficiaries = totalBeneficiaries,
                TotalPayments = totalPayments
            };
        }
    }
}
