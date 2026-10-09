using KadreeBankApplication.Features.Reports.Dtos;
using KadreeBankApplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankApplication.Features.Reports.GetMonthlyAccountStatement
{
    public sealed class GetMonthlyAccountStatementQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetMonthlyAccountStatementQuery, MonthlyAccountStatementDto>
    {
        public async Task<MonthlyAccountStatementDto> Handle(
        GetMonthlyAccountStatementQuery request,
        CancellationToken cancellationToken)
        {
            if (request.AccountId == Guid.Empty)
                throw new ArgumentException(
                    "El identificador de la cuenta es obligatorio.");

            if (request.Year < 1 || request.Year > 9999)
                throw new ArgumentOutOfRangeException(nameof(request.Year));

            if (request.Month is < 1 or > 12)
                throw new ArgumentOutOfRangeException(nameof(request.Month));

            var startDate = new DateTime(
                request.Year, request.Month, 1, 0, 0, 0, DateTimeKind.Utc);

            var endDate = startDate.AddMonths(1);

            var statement = await unitOfWork.Reports
                .GetMonthlyAccountStatementAsync(
                    request.AccountId,
                    startDate,
                    endDate,
                    cancellationToken);

            return statement ?? throw new KeyNotFoundException(
                "No se encontró la cuenta bancaria.");
        }
    }
}
