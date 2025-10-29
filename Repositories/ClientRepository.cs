using Banking_Payments.Models;
using dummy_api.Context;
using Microsoft.EntityFrameworkCore;

namespace Banking_Payments.Repositories
{
    public class ClientRepository : IClientRepository
    {
        public readonly AppDbContext _appDbContext;
        public ClientRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task<Beneficiary> AddBeneficiary(Beneficiary beneficiary)
        {
             _appDbContext.Beneficiaries.Add(beneficiary);
            await _appDbContext.SaveChangesAsync();
            return beneficiary;
        }

        public async Task<IEnumerable<Beneficiary>> ShowBeneficiaries()
        {
            return await _appDbContext.Beneficiaries.ToListAsync();
        }

        public async Task<Payment> AddPayment(Payment payment)
        {
            await _appDbContext.Payments.AddAsync(payment);
            await _appDbContext.SaveChangesAsync();
            return payment;
        }



    }
}
