using KadreeBankApplication.Features.Reports.GetHighValueOutOfCityWithdrawals;
using KadreeBankApplication.Features.Reports.GetMonthlyAccountStatement;
using KadreeBankApplication.Features.Reports.GetMonthlyCustomerTransactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KadreeBankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController(ISender sender) : ControllerBase
    {
        [HttpGet("monthly-customer-transactions")]
        public async Task<IActionResult> GetMonthlyCustomerTransactions(
            [FromQuery] int year,
            [FromQuery] int month,
            CancellationToken cancellationToken)
        {
            var report = await sender.Send(
                new GetMonthlyCustomerTransactionsQuery(year, month),
                cancellationToken);

            return Ok(report);
        }

        [HttpGet("out-of-city-withdrawals")]
        public async Task<IActionResult> GetOutOfCityWithdrawals(
     CancellationToken cancellationToken)
        {
            var report = await sender.Send(
                new GetHighValueOutOfCityWithdrawalsQuery(),
                cancellationToken);

            return Ok(report);
        }

        [HttpGet("monthly-account-statement/{accountId:guid}")]
        public async Task<IActionResult> GetMonthlyAccountStatement(
            Guid accountId,
            [FromQuery] int year,
            [FromQuery] int month,
            CancellationToken cancellationToken)
        {
            var statement = await sender.Send(
                new GetMonthlyAccountStatementQuery(accountId, year, month), cancellationToken);

            return Ok(statement);
        }
    }
}
