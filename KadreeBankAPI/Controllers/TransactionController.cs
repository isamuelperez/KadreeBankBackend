using KadreeBankApplication.Features.Transactions.Commands.Deposit;
using KadreeBankApplication.Features.Transactions.Commands.Queries.GetRecentTransactions;
using KadreeBankApplication.Features.Transactions.Commands.Withdraw;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KadreeBankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController(ISender sender) : ControllerBase
    {
        [HttpPost("Consignar")]
        public async Task<IActionResult> Deposit([FromBody] DepositCommand command, CancellationToken cancellationToken)
        {
            Guid transactionId = await sender.Send(command, cancellationToken);

            return Ok(new { transactionId });
        }

        [HttpPost("Retirar")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawCommand command, CancellationToken cancellationToken)
        {
            Guid transactionId = await sender.Send(command, cancellationToken);

            return Ok(new { transactionId });
        }

        [HttpGet("account/{accountId:guid}/recent")]
        public async Task<IActionResult> GetRecent(
       Guid accountId,
       [FromQuery] int count = 10,
       CancellationToken cancellationToken = default)
        {
            var transactions = await sender.Send(
                new GetRecentTransactionsQuery(accountId, count),
                cancellationToken);

            return Ok(transactions);
        }
    }
}
