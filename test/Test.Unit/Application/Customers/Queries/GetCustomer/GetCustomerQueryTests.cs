using Application.Customers.Queries.Models;
using CrossCutting.Utils;
using FluentValidation;
using MediatR;

namespace Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQueryTests : IRequest<Result<CustomerResponse>>
    {
        public Guid CustomerId { get; private set; }

        public GetCustomerQueryTests(Guid customerId)
        {
            CustomerId = customerId;
        }
    }

    public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQueryTests>
    {
        public GetCustomerQueryValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty();
        }
    }
}
