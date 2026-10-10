using KadreeBankApplication.Features.Transactions.Commands.Deposit;
using KadreeBankApplication.Features.Transactions.Commands.Queries.GetRecentTransactions;
using KadreeBankApplication.Features.Transactions.Commands.Withdraw;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using KadreeBankAPI.Models;

namespace KadreeBankAPI.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController(ISender sender) : BaseApiController
    {
        [HttpPost("Consignar")]
        public async Task<ActionResult<ApiResponse>> Deposit([FromBody] DepositCommand command, CancellationToken cancellationToken)
        {
            Guid transactionId = await sender.Send(command, cancellationToken);

            return Success(new { transactionId }, "Created", 201);
        }

        [HttpPost("Retirar")]
        public async Task<ActionResult<ApiResponse>> Withdraw([FromBody] WithdrawCommand command, CancellationToken cancellationToken)
        {
            Guid transactionId = await sender.Send(command, cancellationToken);

            return Success(new { transactionId }, "Created", 201);
        }

        [HttpGet("account/{accountId:guid}/recent")]
        public async Task<ActionResult<ApiResponse>> GetRecent(
       Guid accountId,
       [FromQuery] int count = 10,
       CancellationToken cancellationToken = default)
        {
            var transactions = await sender.Send(
                new GetRecentTransactionsQuery(accountId, count),
                cancellationToken);

            return Success(transactions);
        }
    }
}
