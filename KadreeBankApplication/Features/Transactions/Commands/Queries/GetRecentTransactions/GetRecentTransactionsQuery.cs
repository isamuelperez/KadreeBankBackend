using MediatR;

namespace KadreeBankApplication.Features.Transactions.Commands.Queries.GetRecentTransactions
{
    public sealed record GetRecentTransactionsQuery(
        Guid AccountId,
        int Count = 10
    ) : IRequest<IReadOnlyList<TransactionDto>>;

    public sealed record TransactionDto(
        Guid Id,
        Guid AccountId,
        string Type,
        decimal Amount,
        DateTime TransactionDate,
        string City,
        decimal BalanceAfter
    );
}
