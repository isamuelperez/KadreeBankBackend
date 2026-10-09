using KadreeBankApplication.Features.Accounts.Commands.OpenAccount;
using KadreeBankApplication.Features.Accounts.Queries.GetAccountById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KadreeBankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AccountController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Open([FromBody] OpenAccountCommand command, CancellationToken cancellationToken)
        {
            Guid accountId = await sender.Send(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = accountId },
                new { id = accountId }
             );
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var account = await sender.Send(new GetAccountByIdQuery(id), cancellationToken);

            return Ok(account);
        }
    }
}
