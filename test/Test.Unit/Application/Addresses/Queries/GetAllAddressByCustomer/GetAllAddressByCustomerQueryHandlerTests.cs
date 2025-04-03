using Application.Addresses.Queries.GetAllAddressByCustomer;
using Application.Mappers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Test.Unit.Application.Addresses.Queries.GetAllByCustomer
{
    public class GetAllAddressByCustomerQueryHandlerTests
    {
        private readonly Mock<IAddressRepository> _addressRepository;

        private IMapper _mapper;

        public GetAllAddressByCustomerQueryHandlerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AddressProfile>();
            });

            _mapper = config.CreateMapper();

            _addressRepository = new Mock<IAddressRepository>();

        }

        [Fact]
        public async Task Handle_WhenAddressIdValid_ReturnSuccess()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var addresses = new List<Address>
            {
                new() { Id = Guid.NewGuid(), CustomerId = customerId, Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" },
                new() { Id = Guid.NewGuid(), CustomerId = customerId, Street = "456 Elm St", City = "Othertown", State = "CA", ZipCode = "67890" }
            };

            _addressRepository
                .Setup(x => x.GetByCustomerIdAsync(customerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(addresses);

            var query = new GetAllAddressByCustomerQuery(customerId);

            var handler = new GetAllAddressByCustomerQueryHandler(_addressRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
            _addressRepository
                .Verify(x => x.GetByCustomerIdAsync(customerId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
