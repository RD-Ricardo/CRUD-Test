using AutoMapper;
using CrossCutting.Utils;
using Domain.Repositories;
using MediatR;

namespace Application.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Result<Unit>>
    {
        private readonly IAddressRepository _addressRepository;

        private readonly ICustomerRepository _customerRepository;

        private readonly IMapper _mapper;

        public CreateAddressCommandHandler(IAddressRepository addressRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);

            if (customer is null)
                return Result<Unit>.Failure(ErrorCatalog.CustomerNotFound);

            var address = _mapper.Map<Domain.Entities.Address>(request);

            address.CustomerId = customer.Id;

            await _addressRepository.AddAsync(address, cancellationToken);

            await _addressRepository.UnitOfWork.Commit();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
