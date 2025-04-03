using Application.Addresses.Models;
using CrossCutting.Utils;
using MediatR;

namespace Application.Addresses.Queries.GetAllByCustomer
{
    public class GetAllAddressByCustomerQuery : IRequest<Result<List<AddressReponse>>>
    {
        public Guid CustomerId { get; private set; }
        public GetAllAddressByCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
