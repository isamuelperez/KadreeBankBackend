using KadreeBankApplication.Features.Reports.Dtos;
using KadreeBankApplication.Interfaces;
using MediatR;

namespace KadreeBankApplication.Features.Reports.GetMonthlyCustomerTransactions
{
    internal class GetMonthlyCustomerTransactionsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetMonthlyCustomerTransactionsQuery,IReadOnlyList<CustomerTransactionReportDto>>
    {
        public async Task<IReadOnlyList<CustomerTransactionReportDto>> Handle(
        GetMonthlyCustomerTransactionsQuery request,
        CancellationToken cancellationToken)
        {
            if (request.Year < 1 || request.Year > 9999)
                throw new ArgumentOutOfRangeException(nameof(request.Year));

            if (request.Month is < 1 or > 12)
                throw new ArgumentOutOfRangeException(nameof(request.Month));

            var startDate = new DateTime(
                request.Year, request.Month, 1, 0, 0, 0, DateTimeKind.Utc);

            var endDate = startDate.AddMonths(1);

            return await unitOfWork.Reports
                .GetMonthlyCustomerTransactionsAsync(startDate,endDate,cancellationToken);
        }
    }
}
