using AutoMapper;
using CrossCutting.Utils;
using Domain.Repositories;
using MediatR;

namespace Application.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, Result<Unit>>
    {
        private readonly IAddressRepository _addressRepository;

        private readonly IMapper _mapper;

        public UpdateAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetByIdAsync(request.GetAddressId(), cancellationToken);

            if (address is null)
                return Result<Unit>.Failure(ErrorCatalog.AddressNotFound);

            _mapper.Map(request, address);

            address.UpdateAt();

            _addressRepository.Update(address, cancellationToken);

            await _addressRepository.UnitOfWork.Commit();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
