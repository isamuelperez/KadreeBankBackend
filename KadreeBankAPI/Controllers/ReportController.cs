using KadreeBankApplication.Features.Reports.GetHighValueOutOfCityWithdrawals;
using KadreeBankApplication.Features.Reports.GetMonthlyAccountStatement;
using KadreeBankApplication.Features.Reports.GetMonthlyCustomerTransactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using KadreeBankAPI.Models;

namespace KadreeBankAPI.Controllers
{
    [Route("api/[controller]")]
    public class ReportController(ISender sender) : BaseApiController
    {
        [HttpGet("monthly-customer-transactions")]
        public async Task<ActionResult<ApiResponse>> GetMonthlyCustomerTransactions(
            [FromQuery] int year,
            [FromQuery] int month,
            CancellationToken cancellationToken)
        {
            var report = await sender.Send(
                new GetMonthlyCustomerTransactionsQuery(year, month),
                cancellationToken);

            return Success(report);
        }

        [HttpGet("out-of-city-withdrawals")]
        public async Task<ActionResult<ApiResponse>> GetOutOfCityWithdrawals(
     CancellationToken cancellationToken)
        {
            var report = await sender.Send(
                new GetHighValueOutOfCityWithdrawalsQuery(),
                cancellationToken);

            return Success(report);
        }

        [HttpGet("monthly-account-statement/{accountId:guid}")]
        public async Task<ActionResult<ApiResponse>> GetMonthlyAccountStatement(
            Guid accountId,
            [FromQuery] int year,
            [FromQuery] int month,
            CancellationToken cancellationToken)
        {
            var statement = await sender.Send(
                new GetMonthlyAccountStatementQuery(accountId, year, month), cancellationToken);

            return Success(statement);
        }
    }
}
