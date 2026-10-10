using KadreeBankApplication.Features.Customers.Commands.CreateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using KadreeBankAPI.Models;

namespace KadreeBankAPI.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController(ISender sender) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Create(
         [FromBody] CreateCustomerCommand command,
         CancellationToken cancellationToken)
        {
            Guid customerId = await sender.Send(command, cancellationToken);

            return CreatedResponse($"/api/customers/{customerId}", new { id = customerId });
        }
    }
}
