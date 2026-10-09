using KadreeBankApplication.Features.Customers.Commands.CreateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KadreeBankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(
         [FromBody] CreateCustomerCommand command,
         CancellationToken cancellationToken)
        {
            Guid customerId = await sender.Send(command, cancellationToken);

            return Created(
                $"/api/customers/{customerId}",
                new { id = customerId }
             );
        }
    }
}
