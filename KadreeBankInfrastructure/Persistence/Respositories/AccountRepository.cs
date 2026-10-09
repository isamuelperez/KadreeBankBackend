using KadreeBankApplication.Interfaces.Repositories;
using KadreeBankDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KadreeBankInfrastructure.Persistence.Respositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly KadreeBankDbContext _context;
        public AccountRepository(KadreeBankDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Account account, CancellationToken cancellationToken = default)
        {
            await _context.Accounts.AddAsync(account, cancellationToken);
        }

        public async Task<Account?> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber, cancellationToken);
        }

        public async Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
    }
}
