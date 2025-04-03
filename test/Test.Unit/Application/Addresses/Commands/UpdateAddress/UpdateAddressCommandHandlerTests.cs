using Application;
using Application.Addresses.Commands.UpdateAddress;
using Application.Mappers;
using AutoMapper;
using Domain.Repositories;
using Moq;

namespace Test.Unit.Application.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandHandlerTests
    {
        private readonly Mock<IAddressRepository> _addressRepository;

        private readonly IMapper _mapper;

        public UpdateAddressCommandHandlerTests()
        {
            _addressRepository = new Mock<IAddressRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AddressProfile());
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_DataValid_ReturnSuccess()
        {
            // Arrange
            var command = new UpdateAddressCommand
            {
                Street = "123 Main St",
                City = "Anytown",
                State = "CA",
                ZipCode = "12345"
            };

            var address = new Domain.Entities.Address
            {
                Id = command.GetAddressId(),
                Street = command.Street,
                City = command.City,
                State = command.State,
                ZipCode = command.ZipCode
            };

            _addressRepository.Setup(repo => repo.GetByIdAsync(command.GetAddressId(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(address);

            _addressRepository.Setup(repo => repo.Update(It.IsAny<Domain.Entities.Address>(), It.IsAny<CancellationToken>()));

            _addressRepository.Setup(repo => repo.UnitOfWork.Commit());

            var handler = new UpdateAddressCommandHandler(_addressRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            _addressRepository.Verify(repo => repo.GetByIdAsync(command.GetAddressId(), It.IsAny<CancellationToken>()), Times.Once);
            _addressRepository.Verify(repo => repo.Update(It.IsAny<Domain.Entities.Address>(), It.IsAny<CancellationToken>()), Times.Once);
            _addressRepository.Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public async Task Handle_AddressNotFound_ReturnFailure()
        {
            // Arrange
            var command = new UpdateAddressCommand
            {
                Street = "123 Main St",
                City = "Anytown",
                State = "CA",
                ZipCode = "12345"
            };

            _addressRepository.Setup(repo => repo.GetByIdAsync(command.GetAddressId(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Address?)null);

            _addressRepository.Setup(repo => repo.Update(It.IsAny<Domain.Entities.Address>(), It.IsAny<CancellationToken>()));

            _addressRepository.Setup(repo => repo.UnitOfWork.Commit());

            var handler = new UpdateAddressCommandHandler(_addressRepository.Object, _mapper);


            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors);
            Assert.Equal(ErrorCatalog.AddressNotFound.Message, result.Errors[0].Message);
            _addressRepository.Verify(repo => repo.GetByIdAsync(command.GetAddressId(), It.IsAny<CancellationToken>()), Times.Once);
            _addressRepository.Verify(repo => repo.Update(It.IsAny<Domain.Entities.Address>(), It.IsAny<CancellationToken>()), Times.Never);
            _addressRepository.Verify(repo => repo.UnitOfWork.Commit(), Times.Never);
        }
    }

}
