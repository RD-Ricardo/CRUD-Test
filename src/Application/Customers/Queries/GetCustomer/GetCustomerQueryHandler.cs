using Application.Customers.Queries.Models;
using CrossCutting.Utils;
using Domain.Repositories;
using MediatR;

namespace Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Result<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Result<CustomerResponse>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);

            if (customer is null)
            {
                return Result<CustomerResponse>.Failure(ErrorCatalog.CustomerNotFound);
            }

            var response = new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Document = customer.Document,
                DocumentType = customer.DocumentType
            };

            return Result<CustomerResponse>.Success(response);
        }
    }
}
