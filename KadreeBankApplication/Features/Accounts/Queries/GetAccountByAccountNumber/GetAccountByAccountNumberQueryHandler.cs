using KadreeBankApplication.Features.Accounts.Queries.GetAccountById;
using KadreeBankApplication.Interfaces;
using MediatR;

namespace KadreeBankApplication.Features.Accounts.Queries.GetAccountByAccountNumber
{
    public sealed class GetAccountByAccountNumberQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetAccountByAccountNumberQuery, AccountDetailsDto>
    {
        public async Task<AccountDetailsDto> Handle(
        GetAccountByAccountNumberQuery request,
        CancellationToken cancellationToken)
        {
            var account = await unitOfWork.Accounts.GetByAccountNumberAsync(
                request.AccountNumber,
                cancellationToken);

            if (account is null)
            {
                return null!;
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
