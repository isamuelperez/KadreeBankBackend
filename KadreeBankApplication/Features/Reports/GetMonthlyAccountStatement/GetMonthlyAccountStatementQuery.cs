using KadreeBankApplication.Features.Reports.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Reports.GetMonthlyAccountStatement
{
    public sealed record GetMonthlyAccountStatementQuery(
        Guid AccountId,
        int Year,
        int Month
    ) : IRequest<MonthlyAccountStatementDto>;
}
