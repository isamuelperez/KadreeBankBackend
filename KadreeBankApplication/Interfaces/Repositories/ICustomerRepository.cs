using KadreeBankDomain.Entities;

namespace KadreeBankApplication.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer, CancellationToken cancellationToken);

        Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<Customer?> GetByIdentificationAsync(string identification, CancellationToken cancellationToken);
    }
}
