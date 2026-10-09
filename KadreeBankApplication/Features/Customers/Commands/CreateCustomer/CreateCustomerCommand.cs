using KadreeBankDomain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Customers.Commands.CreateCustomer
{
    public sealed record CreateCustomerCommand(
        string FullName,
        string Identification,
        CustomerType Type
    ) : IRequest<Guid>;
}
