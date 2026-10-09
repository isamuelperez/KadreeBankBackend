using KadreeBankApplication.Features.Reports.Dtos;

namespace KadreeBankApplication.Interfaces.Repositories
{
    public interface IReportRepository
    {
        Task<IReadOnlyList<CustomerTransactionReportDto>>
            GetMonthlyCustomerTransactionsAsync(
            DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default);
        
        Task<IReadOnlyList<OutOfCityWithdrawalReportDto>>
            GetOutOfCityWithdrawalsAsync(decimal minimumAmount,CancellationToken cancellationToken = default);

        Task<MonthlyAccountStatementDto?>
            GetMonthlyAccountStatementAsync(
            Guid accountId,DateTime startDate,
            DateTime endDate,
            CancellationToken cancellationToken = default);
    }
}
