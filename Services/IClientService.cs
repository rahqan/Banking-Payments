using Banking_Payments.Models;

namespace Banking_Payments.Services
{
    public interface IClientService
    {
        public Task<Beneficiary> AddBeneficiary(Beneficiary beneficiary);

        public Task<IEnumerable<Beneficiary>> ShowBeneficiaries();
        public Task<Payment> AddPayment(Payment payment);
    }
}
