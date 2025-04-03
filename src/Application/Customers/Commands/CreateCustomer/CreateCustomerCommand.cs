using CrossCutting.Utils;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<Result<Unit>>
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Document { get; set; } = null!;
        public DocumentTypeEnum DocumentType { get; set; }
    }

    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Document)
                .NotEmpty();

            RuleFor(x => x.DocumentType)
                .IsInEnum();
        }
    }
}
