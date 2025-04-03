using CrossCutting.Utils;
using FluentValidation;
using MediatR;

namespace Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandTests : IRequest<Result<Unit>>
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        private Guid _customerId;
        public void SetCustomerId(Guid customerId) { _customerId = customerId; }
        public Guid GetCustomerId() => _customerId;
    }

    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommandTests>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
