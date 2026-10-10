using KadreeBankApplication.Features.Accounts.Commands.OpenAccount;
using KadreeBankApplication.Features.Accounts.Queries.GetAccountByAccountNumber;
using KadreeBankApplication.Features.Accounts.Queries.GetAccountById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using KadreeBankAPI.Models;

namespace KadreeBankAPI.Controllers
{
    [Route("api/[controller]")]

    public class AccountController(ISender sender) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Open([FromBody] OpenAccountCommand command, CancellationToken cancellationToken)
        {
            Guid accountId = await sender.Send(command, cancellationToken);

            return CreatedAtActionResponse(
                nameof(GetById),
                new { id = accountId },
                new { id = accountId }
             );
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ApiResponse>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var account = await sender.Send(new GetAccountByIdQuery(id), cancellationToken);

            return Success(account);
        }

        [HttpGet("{accountNumber}")]
        public async Task<ActionResult<ApiResponse>> GetByAccountNumber(string accountNumber, CancellationToken cancellationToken)
        {
            var account = await sender.Send(
                new GetAccountByAccountNumberQuery(accountNumber),
                cancellationToken);

            if (account is null)
            {
                return NotFoundResponse($"No se encontró una cuenta con el número '{accountNumber}'.");
            }

            return Success(account);
        }
    }
}
