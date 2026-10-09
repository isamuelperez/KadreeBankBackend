using KadreeBankDomain.Entities;

namespace KadreeBankApplication.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account, CancellationToken cancellationToken = default);

        Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Account?> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken = default);
    }
}
