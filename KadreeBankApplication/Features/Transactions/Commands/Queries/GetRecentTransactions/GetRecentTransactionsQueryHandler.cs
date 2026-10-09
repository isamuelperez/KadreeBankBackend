using KadreeBankApplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Transactions.Commands.Queries.GetRecentTransactions
{
    public class GetRecentTransactionsQueryHandler(IUnitOfWork unitOfWork) :
        IRequestHandler<GetRecentTransactionsQuery, IReadOnlyList<TransactionDto>>
    {
        public async Task<IReadOnlyList<TransactionDto>> Handle(GetRecentTransactionsQuery request, CancellationToken cancellationToken)
        {
            if (request.AccountId == Guid.Empty)
            {
                throw new ArgumentException("El identificador de la cuenta es obligatorio.");
            }

            if (request.Count is < 1 or > 100)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(request.Count),
                    "La cantidad debe estar entre 1 y 100.");
            }

            var transactions = await unitOfWork.Transactions.GetRecentByAccountIdAsync(
                    request.AccountId,
                    request.Count,
                    cancellationToken
             );

            return transactions
                .Select(transaction => new TransactionDto(
                    transaction.Id,
                    transaction.AccountId,
                    transaction.Type.ToString(),
                    transaction.Amount,
                    transaction.TransactionDate,
                    transaction.City,
                    transaction.BalanceAfter))
                .ToList();
        }
    }
}
