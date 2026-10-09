using KadreeBankApplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Transactions.Commands.Withdraw
{
    public sealed class WithdrawCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<WithdrawCommand, Guid>
    {
        public async Task<Guid> Handle(
        WithdrawCommand request,
        CancellationToken cancellationToken)
        {
            var account = await unitOfWork.Accounts.GetByIdAsync(
                request.AccountId,
                cancellationToken);

            if (account is null)
            {
                throw new KeyNotFoundException(
                    "No se encontró la cuenta bancaria.");
            }

            var transaction = account.Withdraw(
                request.Amount,
                request.City);

            await unitOfWork.Transactions.AddAsync(
                transaction,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return transaction.Id;
        }
    }
}
