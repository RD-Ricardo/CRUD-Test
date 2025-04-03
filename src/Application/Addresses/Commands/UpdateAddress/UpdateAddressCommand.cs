using CrossCutting.Utils;
using MediatR;

namespace Application.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommand : IRequest<Result<Unit>>
    {
        public string Street { get; set; } = null!;
        public string Number { get; set; } = null!;
        public string? Complement { get; set; }
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;

        private Guid addressId;
        public Guid GetAddressId() => addressId;
        public void SetAddressId(Guid id) => addressId = id;
    }
}
