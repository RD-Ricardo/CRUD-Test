using Application.Addresses.Models;
using AutoMapper;
using CrossCutting.Utils;
using Domain.Repositories;
using MediatR;

namespace Application.Addresses.Queries.GetAllByCustomer
{
    public class GetAllAddressByCustomerQueryHandler : IRequestHandler<GetAllAddressByCustomerQuery, Result<List<AddressReponse>>>
    {
        private readonly IAddressRepository _addressRepository;

        private readonly IMapper _mapper;
        public GetAllAddressByCustomerQueryHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<AddressReponse>>> Handle(GetAllAddressByCustomerQuery request, CancellationToken cancellationToken)
        {
            var addresses = await _addressRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

            if (addresses is null)
                return Result<List<AddressReponse>>.Failure(ErrorCatalog.NoAddressesFound);

            var addressResponses = _mapper.Map<List<AddressReponse>>(addresses);

            return Result<List<AddressReponse>>.Success(addressResponses);
        }
    }
}
