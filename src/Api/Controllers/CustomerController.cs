using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.DeleteCustomer;
using Application.Customers.Commands.UpdateCustomer;
using Application.Customers.Queries.GetAllCustomer;
using Application.Customers.Queries.GetCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : BaseController
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var result = await _mediator.Send(new GetCustomerQuery(id));
            return CustomReponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _mediator.Send(new GetAllCustomerQuery());
            return CustomReponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return CustomReponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            command.SetCustomerId(id);
            var result = await _mediator.Send(command);
            return CustomReponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id));
            return CustomReponse(result);
        }
    }
}
