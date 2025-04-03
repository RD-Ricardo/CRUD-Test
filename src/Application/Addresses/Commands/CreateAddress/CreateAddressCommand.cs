using CrossCutting.Utils;
using FluentValidation;
using MediatR;

namespace Application.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommand : IRequest<Result<Unit>>
    {
        public Guid CustomerId { get; set; }
        public string Street { get; set; } = null!;
        public string Number { get; set; } = null!;
        public string? Complement { get; set; }
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
    }

    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty();

            RuleFor(x => x.Street)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Number)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.Complement)
                .MaximumLength(50);

            RuleFor(x => x.ZipCode)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.City)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.State)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
