using Application.Customers.Queries.Models;
using CrossCutting.Utils;
using Domain.Repositories;
using MediatR;

namespace Application.Customers.Queries.GetAllCustomer
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, Result<List<CustomerResponse>>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetAllCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<List<CustomerResponse>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync(cancellationToken);

            var response = customers.Select(c => new CustomerResponse
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Document = c.Document,
                DocumentType = c.DocumentType
            });

            return Result<List<CustomerResponse>>.Success(response.ToList());
        }
    }
}
