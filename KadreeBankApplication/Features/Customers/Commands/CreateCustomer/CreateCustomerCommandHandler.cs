using KadreeBankApplication.Interfaces;
using KadreeBankDomain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCustomerCommand, Guid>
    {
        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            string identification = request.Identification.Trim();

            var existingCustomer =
                await unitOfWork.Customers.GetByIdentificationAsync(identification, cancellationToken);

            if (existingCustomer is not null)
            {
                throw new InvalidOperationException(
                    "Ya existe un cliente con esa identificación.");
            }

            var customer = new Customer(
                request.FullName,
                identification,
                request.Type
             );

            await unitOfWork.Customers.AddAsync(customer,cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return customer.Id;
        }
    }
}
