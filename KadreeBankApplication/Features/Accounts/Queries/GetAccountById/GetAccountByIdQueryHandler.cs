using KadreeBankApplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Accounts.Queries.GetAccountById
{
    public sealed class GetAccountByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAccountByIdQuery, AccountDetailsDto>
    {
        public async Task<AccountDetailsDto> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await unitOfWork.Accounts.GetByIdAsync(
                request.accountId,
                cancellationToken);

            if (account is null)
            {
                throw new KeyNotFoundException(
                    "No se encontró la cuenta bancaria.");
            }

            return new AccountDetailsDto(
                account.Id,
                account.AccountNumber,
                account.IdCustomer,
                account.Type.ToString(),
                account.Balance,
                account.OriginCity,
                account.CreatedAt);
        }
    }
}
