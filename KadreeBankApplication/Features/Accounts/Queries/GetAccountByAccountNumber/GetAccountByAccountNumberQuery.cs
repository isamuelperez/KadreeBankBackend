using KadreeBankApplication.Features.Accounts.Queries.GetAccountById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Accounts.Queries.GetAccountByAccountNumber
{
    public sealed record GetAccountByAccountNumberQuery(string AccountNumber)
        : IRequest<AccountDetailsDto>;
}
