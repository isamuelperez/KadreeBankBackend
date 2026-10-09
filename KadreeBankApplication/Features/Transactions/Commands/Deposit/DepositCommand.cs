using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Transactions.Commands.Deposit
{
    public sealed record DepositCommand(
        Guid AccountId,
        decimal Amount,
        string City
    ) : IRequest<Guid>;
}
