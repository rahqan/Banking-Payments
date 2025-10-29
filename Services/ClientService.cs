using Banking_Payments.Models;
using Banking_Payments.Repositories;

namespace Banking_Payments.Services
{
    public class ClientService : IClientService
    {
        public readonly IClientRepository clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public async Task<Beneficiary> AddBeneficiary(Beneficiary beneficiary)
        {
            return await clientRepository.AddBeneficiary(beneficiary);
        }

        public async Task<IEnumerable<Beneficiary>> ShowBeneficiaries()
        {
            return await clientRepository.ShowBeneficiaries();
        }

        public async Task<Payment> AddPayment(Payment payment)
        {
            return await clientRepository.AddPayment(payment);
        }
    }
}
