using Application.Customers.Queries.Models;
using CrossCutting.Utils;
using FluentValidation;
using MediatR;

namespace Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQuery : IRequest<Result<CustomerResponse>>
    {
        public Guid CustomerId { get; private set; }

        public GetCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }

    public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
    {
        public GetCustomerQueryValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty();
        }
    }
}
