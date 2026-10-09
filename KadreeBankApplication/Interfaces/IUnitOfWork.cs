using KadreeBankApplication.Interfaces.Repositories;

namespace KadreeBankApplication.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        IAccountRepository Accounts { get; }
        ITransactionRepository Transactions { get; }
        IReportRepository Reports { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
