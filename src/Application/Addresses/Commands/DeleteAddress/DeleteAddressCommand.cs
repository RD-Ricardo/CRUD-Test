using CrossCutting.Utils;
using MediatR;

namespace Application.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommand : IRequest<Result<Unit>>
    {
        public Guid AddressId { get; private set; }
        public DeleteAddressCommand(Guid addressId)
        {
            AddressId = addressId;
        }
    }
}
