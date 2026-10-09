using KadreeBankApplication.Interfaces;
using KadreeBankDomain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Accounts.Commands.OpenAccount
{
    public sealed class OpenAccountCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<OpenAccountCommand, Guid>
    {
        public async Task<Guid> Handle(OpenAccountCommand request, CancellationToken cancellationToken)
        {
            var customer = await unitOfWork.Customers.GetByIdAsync(request.CustomerId, cancellationToken);

            if (customer is null)
            {
                throw new KeyNotFoundException("No se encontró el cliente indicado.");
            }

            string accountNumber = request.AccountNumber.Trim();

            var existingAccount =
                await unitOfWork.Accounts.GetByAccountNumberAsync(accountNumber, cancellationToken);

            if (existingAccount is not null)
            {
                throw new InvalidOperationException("Ya existe una cuenta con ese número.");
            }

            var account = Account.Open(
                customer,
                accountNumber,
                request.Type,
                request.OriginCity
             );

            await unitOfWork.Accounts.AddAsync(account, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return account.Id;
        }
    }
}
