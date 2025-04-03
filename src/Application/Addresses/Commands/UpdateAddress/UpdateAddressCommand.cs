using CrossCutting.Utils;
using FluentValidation;
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

    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(x => x.Street)
                .NotEmpty()
                .WithMessage("Street is required.");
            RuleFor(x => x.Number)
                .NotEmpty()
                .WithMessage("Number is required.");

            RuleFor(x => x.ZipCode)
                .NotEmpty()
                .WithMessage("ZipCode is required.")
                .Matches(@"^\d{5}-\d{4}$")
                .WithMessage("ZipCode must be in the format 12345-6789.");

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("City is required.");

            RuleFor(x => x.State)
                .NotEmpty()
                .WithMessage("State is required.");

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Country is required.");
        }
    }
}
