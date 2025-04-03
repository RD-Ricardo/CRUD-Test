using CrossCutting.Utils;
using FluentValidation;
using MediatR;

namespace Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<Result<Unit>>
    {
        public Guid CustomerId { get; private set; }
        public DeleteCustomerCommand(Guid customerId)
        {
            CustomerId = customerId;
        }
    }

    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty();
        }
    }
}
