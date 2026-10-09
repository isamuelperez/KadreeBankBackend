using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Accounts.Queries.GetAccountById
{
    public sealed record GetAccountByIdQuery(Guid accountId): 
        IRequest<AccountDetailsDto>;

    public sealed record AccountDetailsDto(
        Guid Id,
        string AccountNumber,
        Guid CustomerId,
        string Type,
        decimal Balance,
        string OriginCity,
        DateTime CreatedAt
    );
}
