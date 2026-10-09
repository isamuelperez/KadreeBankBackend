using KadreeBankDomain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Accounts.Commands.OpenAccount
{
    public sealed record  OpenAccountCommand(
        Guid CustomerId,
        string AccountNumber,
        AccountType Type,
        string OriginCity
    ) : IRequest<Guid>;
}
