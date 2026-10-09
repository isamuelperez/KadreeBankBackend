using KadreeBankApplication.Features.Reports.Dtos;
using KadreeBankApplication.Interfaces;
using MediatR;

namespace KadreeBankApplication.Features.Reports.GetHighValueOutOfCityWithdrawals
{
    public class GetHighValueOutOfCityWithdrawalsQueryHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<
        GetHighValueOutOfCityWithdrawalsQuery,
        IReadOnlyList<OutOfCityWithdrawalReportDto>>
    {
        public async Task<IReadOnlyList<OutOfCityWithdrawalReportDto>> Handle(
       GetHighValueOutOfCityWithdrawalsQuery request,
       CancellationToken cancellationToken)
        {
            return await unitOfWork.Reports.GetOutOfCityWithdrawalsAsync(
                minimumAmount: 1_000_000m,
                cancellationToken: cancellationToken);
        }
    }
}
