using Banking_Payments.Models;
using Banking_Payments.Models.DTOs;
using Banking_Payments.Repositories;

namespace Banking_Payments.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _repo;

        public BankService(IBankRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<BankDTO>> GetAllAsync()
        {
            var banks = await _repo.GetAllAsync();
            return banks.Select(MapToDTO);
        }

        public async Task<BankDTO?> GetByIdAsync(int id)
        {
            var bank = await _repo.GetByIdAsync(id);
            return bank == null ? null : MapToDTO(bank);
        }

        //public async Task<BankDTO> CreateAsync(CreateBankDTO dto)
        //{
        //    var bank = new Bank
        //    {
        //        Code = dto.Code,
        //        Name = dto.Name,
        //        Address = dto.Address,
        //        PanNumber = dto.PanNumber,
        //        RegistrationNumber = dto.RegistrationNumber,
        //        ContactEmail = dto.ContactEmail,
        //        ContactPhone = dto.ContactPhone,
        //        CreatedByAdminId = dto.CreatedByAdminId,
        //        IsActive = true
        //    };

        //    await _repo.AddAsync(bank);
        //    await _repo.SaveChangesAsync();

        //    return MapToDTO(bank);
        //}


        public async Task<BankDTO> CreateAsync(CreateBankDTO dto, int createdByAdminId)
        {
            var bank = new Bank
            {
                Code = dto.Code,
                Name = dto.Name,
                Address = dto.Address,
                PanNumber = dto.PanNumber,
                RegistrationNumber = dto.RegistrationNumber,
                ContactEmail = dto.ContactEmail,
                ContactPhone = dto.ContactPhone,
                CreatedByAdminId = createdByAdminId,
                IsActive = true
            };

            await _repo.AddAsync(bank);
            await _repo.SaveChangesAsync();

            return MapToDTO(bank);
        }



        public async Task<bool> UpdateAsync(int id, UpdateBankDTO dto)
        {
            var bank = await _repo.GetByIdAsync(id);
            if (bank == null) return false;

            bank.Name = dto.Name;
            bank.Address = dto.Address;
            bank.PanNumber = dto.PanNumber;
            bank.RegistrationNumber = dto.RegistrationNumber;
            bank.ContactEmail = dto.ContactEmail;
            bank.ContactPhone = dto.ContactPhone;
            bank.IsActive = dto.IsActive;

            await _repo.UpdateAsync(bank);
            await _repo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var bank = await _repo.GetByIdAsync(id);
            if (bank == null) return false;

            await _repo.SoftDeleteAsync(id);
            await _repo.SaveChangesAsync();
            return true;
        }

        // 🧭 Mapping Method
        private static BankDTO MapToDTO(Bank bank)
        {
            return new BankDTO
            {
                BankId = bank.BankId,
                Code = bank.Code,
                Name = bank.Name,
                Address = bank.Address,
                PanNumber = bank.PanNumber,
                RegistrationNumber = bank.RegistrationNumber,
                ContactEmail = bank.ContactEmail,
                ContactPhone = bank.ContactPhone,
                IsActive = bank.IsActive,
                CreatedAt = bank.CreatedAt,
                CreatedByAdminId = bank.CreatedByAdminId
            };
        }
    }
}
