using Application;
using Application.Addresses.Commands.DeleteAddress;
using Domain.Repositories;
using Moq;

namespace Test.Unit.Application.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommandHandlerTests
    {
        private readonly Mock<IAddressRepository> _addressRepository;
        public DeleteAddressCommandHandlerTests()
        {
            _addressRepository = new Mock<IAddressRepository>();
        }

        [Fact]
        public async Task Handle_AddressIdValid_ReturnSuccess()
        {
            // Arrange
            var addressId = Guid.NewGuid();

            var command = new DeleteAddressCommand(addressId);

            var address = new Domain.Entities.Address
            {
                Id = command.AddressId,
                Street = "123 Main St",
                City = "Anytown",
                State = "CA",
                ZipCode = "12345"
            };

            _addressRepository.Setup(repo => repo.GetByIdAsync(command.AddressId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(address);

            _addressRepository.Setup(repo => repo.Delete(It.IsAny<Domain.Entities.Address>(), It.IsAny<CancellationToken>()));

            _addressRepository.Setup(repo => repo.UnitOfWork.Commit());

            var handler = new DeleteAddressCommandHandler(_addressRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _addressRepository.Verify(repo => repo.GetByIdAsync(command.AddressId, It.IsAny<CancellationToken>()), Times.Once);
            _addressRepository.Verify(repo => repo.Delete(It.IsAny<Domain.Entities.Address>(), It.IsAny<CancellationToken>()), Times.Once);
            _addressRepository.Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public async Task Handle_AddressIdInvalid_ReturnFailure()
        {
            // Arrange
            var addressId = Guid.NewGuid();

            var command = new DeleteAddressCommand(addressId);

            _addressRepository.Setup(repo => repo.GetByIdAsync(command.AddressId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Address?)null);

            _addressRepository.Setup(repo => repo.Delete(It.IsAny<Domain.Entities.Address>(), It.IsAny<CancellationToken>()));

            _addressRepository.Setup(repo => repo.UnitOfWork.Commit());

            var handler = new DeleteAddressCommandHandler(_addressRepository.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors);
            Assert.Equal(ErrorCatalog.AddressNotFound.Message, result.Errors[0].Message);
            _addressRepository.Verify(repo => repo.GetByIdAsync(command.AddressId, It.IsAny<CancellationToken>()), Times.Once);
            _addressRepository.Verify(repo => repo.Delete(It.IsAny<Domain.Entities.Address>(), It.IsAny<CancellationToken>()), Times.Never);
            _addressRepository.Verify(repo => repo.UnitOfWork.Commit(), Times.Never);
        }

    }
}
