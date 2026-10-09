using KadreeBankApplication.Interfaces;
using KadreeBankApplication.Interfaces.Repositories;
using KadreeBankInfrastructure.Persistence.Respositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankInfrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly KadreeBankDbContext _context;
        public ICustomerRepository Customers { get; }
        public IAccountRepository Accounts { get; }
        public ITransactionRepository Transactions { get; }
        public IReportRepository Reports { get; }

        public UnitOfWork(KadreeBankDbContext context, ICustomerRepository customerRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository, IReportRepository reports)
        {
            _context = context;
            Customers = customerRepository;
            Accounts = accountRepository;
            Transactions = transactionRepository;
            Reports = reports;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
