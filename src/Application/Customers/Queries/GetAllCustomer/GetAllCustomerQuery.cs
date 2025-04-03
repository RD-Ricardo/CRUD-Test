using Application.Customers.Queries.Models;
using CrossCutting.Utils;
using MediatR;

namespace Application.Customers.Queries.GetAllCustomer
{
    public class GetAllCustomerQuery : IRequest<Result<List<CustomerResponse>>>;
}
