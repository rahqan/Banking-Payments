using Banking_Payments.Models;

namespace Banking_Payments.Repositories
{
    public interface IClientRepository
    {
        public Task<Beneficiary> AddBeneficiary(Beneficiary beneficiary);
        public Task<IEnumerable<Beneficiary>> ShowBeneficiaries();
        public Task<Payment> AddPayment(Payment payment);
    }
}
