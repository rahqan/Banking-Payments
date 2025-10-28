using dummy_api.Models;

namespace dummy_api.Repositories
{
    public interface IClientRepository
    {
        public Task<Beneficiary> AddBeneficiary(Beneficiary beneficiary);
        public Task<IEnumerable<Beneficiary>> ShowBeneficiaries();
        public Task<Payment> AddPayment(Payment payment);
    }
}
