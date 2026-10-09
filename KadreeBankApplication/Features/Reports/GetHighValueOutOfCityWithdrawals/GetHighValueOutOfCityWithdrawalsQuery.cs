using KadreeBankApplication.Features.Reports.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Reports.GetHighValueOutOfCityWithdrawals
{
    
    public sealed record GetHighValueOutOfCityWithdrawalsQuery : IRequest<IReadOnlyList<OutOfCityWithdrawalReportDto>>;
}
