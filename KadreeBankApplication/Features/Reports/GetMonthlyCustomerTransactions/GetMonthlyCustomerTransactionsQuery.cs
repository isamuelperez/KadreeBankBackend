using KadreeBankApplication.Features.Reports.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Reports.GetMonthlyCustomerTransactions
{
    public sealed record GetMonthlyCustomerTransactionsQuery(
        int Year,
        int Month
    ) : IRequest<IReadOnlyList<CustomerTransactionReportDto>>;
}
