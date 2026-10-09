using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Reports.Dtos
{
    public sealed record CustomerTransactionReportDto(
        Guid CustomerId,
        string CustomerName,
        int TransactionCount
    );

    public sealed record OutOfCityWithdrawalReportDto(
        Guid CustomerId,
        string CustomerName,
        decimal TotalWithdrawals,
        int WithdrawalCount
    );

    public sealed record MonthlyAccountStatementDto(
        Guid AccountId,
        string AccountNumber,
        string CustomerName,
        DateTime PeriodStart,
        DateTime PeriodEnd,
        decimal CurrentBalance,
        decimal TotalDeposits,
        decimal TotalWithdrawals,
        IReadOnlyList<AccountStatementMovementDto> Movements
    );

    public sealed record AccountStatementMovementDto(
        Guid TransactionId,
        DateTime TransactionDate,
        string Type,
        decimal Amount,
        string City,
        decimal BalanceAfter
    );
}
