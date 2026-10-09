using KadreeBankApplication.Interfaces;
using MediatR;

namespace KadreeBankApplication.Features.Transactions.Commands.Deposit
{
    public sealed class DepositCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DepositCommand, Guid>
    {
        public async Task<Guid> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var account = await unitOfWork.Accounts.GetByIdAsync(request.AccountId, cancellationToken);

            if (account is null)
            {
                throw new KeyNotFoundException(
                    "No se encontró la cuenta bancaria.");
            }

            var transaction = account.Deposit(request.Amount, request.City);

            await unitOfWork.Transactions.AddAsync(transaction, cancellationToken);

            try
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw new InvalidOperationException(
                    "La cuenta fue modificada por otra operación. " +
                    "Consulta el saldo actualizado e inténtalo nuevamente.");
            }

            return transaction.Id;
        }
    }
}
