using KadreeBankApplication.Interfaces.Repositories;
using KadreeBankDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KadreeBankInfrastructure.Persistence.Respositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly KadreeBankDbContext _context;

        public TransactionRepository(KadreeBankDbContext context)
        {
            _context = context;
        }

        public async Task<BankTransaction?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
        {
            return await _context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    transaction => transaction.Id == id,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<BankTransaction>> GetRecentByAccountIdAsync(
            Guid accountId,
            int count,
            CancellationToken cancellationToken = default)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            return await _context.Transactions
                .AsNoTracking()
                .Where(transaction => transaction.AccountId == accountId)
                .OrderByDescending(transaction => transaction.TransactionDate)
                .Take(count)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<BankTransaction>> GetByAccountAndPeriodAsync(
            Guid accountId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default)
        {
            if (startDate >= endDate)
                throw new ArgumentException("La fecha inicial debe ser anterior a la fecha final.");

            return await _context.Transactions
                .AsNoTracking()
                .Where(transaction =>
                    transaction.AccountId == accountId &&
                    transaction.TransactionDate >= startDate &&
                    transaction.TransactionDate < endDate)
                .OrderByDescending(transaction => transaction.TransactionDate)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(BankTransaction transaction, CancellationToken cancellationToken = default)
        {
            await _context.Transactions.AddAsync(transaction, cancellationToken);
        }

    }
}
