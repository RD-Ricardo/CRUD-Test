using Application;
using Application.Addresses.Commands.CreateAddress;
using Application.Mappers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using Test.Unit.Application.Customers;

namespace Test.Unit.Application.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepository;

        private readonly Mock<IAddressRepository> _addressRepository;

        private readonly IMapper _mapper;
        public CreateAddressCommandHandlerTests()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _addressRepository = new Mock<IAddressRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AddressProfile());
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Handle_DataAndCustomerValid_ReturnSuccess()
        {
            // Arrange
            var command = new CreateAddressCommand
            {
                CustomerId = Guid.NewGuid(),
                Street = "123 Main St",
                City = "Anytown",
                State = "CA",
                ZipCode = "12345"
            };

            var customer = CustomersTests.ReturnValid();

            _customerRepository.Setup(repo => repo.GetByIdAsync(command.CustomerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);

            _addressRepository.Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.Address>(), It.IsAny<CancellationToken>()));

            _addressRepository.Setup(repo => repo.UnitOfWork.Commit());

            var handler = new CreateAddressCommandHandler(_addressRepository.Object, _customerRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Handle_CustomerNotFound_ReturnFailure()
        {
            // Arrange
            var command = new CreateAddressCommand
            {
                CustomerId = Guid.Empty,
                Street = "123 Main St",
                City = "Anytown",
                State = "CA",
                ZipCode = "12345"
            };

            _customerRepository.Setup(repo => repo.GetByIdAsync(command.CustomerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Customer?)null);

            var handler = new CreateAddressCommandHandler(_addressRepository.Object, _customerRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors);
            Assert.Equal(ErrorCatalog.CustomerNotFound.Message, result.Errors[0].Message);
        }
    }
}
