using KadreeBankApplication.Features.Reports.Dtos;
using KadreeBankApplication.Interfaces.Repositories;
using KadreeBankDomain.Enums;
using Microsoft.EntityFrameworkCore;

namespace KadreeBankInfrastructure.Persistence.Respositories
{
    public sealed class ReportRepository(KadreeBankDbContext context) : IReportRepository
    {
        public async Task<IReadOnlyList<CustomerTransactionReportDto>>
            GetMonthlyCustomerTransactionsAsync(
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default)
        {
            var results = await context.Transactions
                .AsNoTracking()
                .Where(t =>
                    t.TransactionDate >= startDate &&
                    t.TransactionDate < endDate)
                .GroupBy(t => new
                {
                    CustomerId = t.Account.IdCustomer,
                    CustomerName = t.Account.Customer.FullName
                })
                .Select(group => new
                {
                    group.Key.CustomerId,
                    group.Key.CustomerName,
                    TransactionCount = group.Count()
                })
                .OrderByDescending(result => result.TransactionCount)
                .ThenBy(result => result.CustomerName)
                .ToListAsync(cancellationToken);

            return results
                .Select(result => new CustomerTransactionReportDto(
                    result.CustomerId,
                    result.CustomerName,
                    result.TransactionCount))
                .ToList();
        }

        public async Task<IReadOnlyList<OutOfCityWithdrawalReportDto>>
            GetOutOfCityWithdrawalsAsync(
            decimal minimumAmount,
            CancellationToken cancellationToken = default)
        {
            var report = await context.Transactions
                .AsNoTracking()
                .Where(t =>
                    t.Type == TransactionType.Retiro &&
                    t.City != t.Account.OriginCity)
                .GroupBy(t => new
                {
                    t.Account.IdCustomer,
                    t.Account.Customer.FullName
                })
                .Select(group => new
                {
                    group.Key.IdCustomer,
                    group.Key.FullName,
                    TotalWithdrawals = group.Sum(t => t.Amount),
                    WithdrawalCount = group.Count()
                })
                .Where(group => group.TotalWithdrawals > minimumAmount)
                .OrderByDescending(group => group.TotalWithdrawals)
                .ThenBy(group => group.FullName)
                .ToListAsync(cancellationToken);

            return report
                .Select(group => new OutOfCityWithdrawalReportDto(
                    group.IdCustomer,
                    group.FullName,
                    group.TotalWithdrawals,
                    group.WithdrawalCount))
                .ToList();
        }

        public async Task<MonthlyAccountStatementDto?>
            GetMonthlyAccountStatementAsync(
            Guid accountId,
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default)
        {
            var account = await context.Accounts
                .AsNoTracking()
                .Where(a => a.Id == accountId)
                .Select(a => new
                {
                    a.Id,
                    a.AccountNumber,
                    CurrentBalance = a.Balance,
                    CustomerName = a.Customer.FullName
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (account is null)
                return null;

            var movements = await context.Transactions
                .AsNoTracking()
                .Where(t =>
                    t.AccountId == accountId &&
                    t.TransactionDate >= startDate &&
                    t.TransactionDate < endDate)
                .OrderBy(t => t.TransactionDate)
                .ThenBy(t => t.Id)
                .Select(t => new AccountStatementMovementDto(
                    t.Id,
                    t.TransactionDate,
                    t.Type.ToString(),
                    t.Amount,
                    t.City,
                    t.BalanceAfter))
                .ToListAsync(cancellationToken);

            decimal totalDeposits = movements
                .Where(m => m.Type == nameof(TransactionType.Consignacion))
                .Sum(m => m.Amount);

            decimal totalWithdrawals = movements
                .Where(m => m.Type == nameof(TransactionType.Retiro))
                .Sum(m => m.Amount);

            return new MonthlyAccountStatementDto(
                account.Id,
                account.AccountNumber,
                account.CustomerName,
                startDate,
                endDate,
                account.CurrentBalance,
                totalDeposits,
                totalWithdrawals,
                movements);
        }
    }
}
