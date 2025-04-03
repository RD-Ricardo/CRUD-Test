using CrossCutting.Utils;
using Domain.Repositories;
using MediatR;

namespace Application.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, Result<Unit>>
    {
        private readonly IAddressRepository _addressRepository;
        public DeleteAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Result<Unit>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetByIdAsync(request.AddressId, cancellationToken);
            if (address is null)
                return Result<Unit>.Failure(ErrorCatalog.AddressNotFound);

            _addressRepository.Delete(address, cancellationToken);

            await _addressRepository.UnitOfWork.Commit();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
