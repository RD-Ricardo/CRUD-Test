using Application.Addresses.Commands.CreateAddress;
using Application.Addresses.Commands.DeleteAddress;
using Application.Addresses.Commands.UpdateAddress;
using Application.Addresses.Queries.GetAllAddressByCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : BaseController
    {
        private readonly IMediator _mediator;
        public AddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetAllAddressesByCustomer(Guid customerId)
        {
            var result = await _mediator.Send(new GetAllAddressByCustomerQuery(customerId));
            return CustomReponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand command)
        {
            var result = await _mediator.Send(command);
            return CustomReponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] UpdateAddressCommand command)
        {
            command.SetAddressId(id);
            var result = await _mediator.Send(command);
            return CustomReponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            var result = await _mediator.Send(new DeleteAddressCommand(id));
            return CustomReponse(result);
        }
    }
}
