using KadreeBankDomain.Entities;

namespace KadreeBankApplication.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task<BankTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<BankTransaction>> GetRecentByAccountIdAsync(Guid accountId, int count, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<BankTransaction>> GetByAccountAndPeriodAsync(Guid accountId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

        Task AddAsync(BankTransaction transaction, CancellationToken cancellationToken = default);
    }
}
