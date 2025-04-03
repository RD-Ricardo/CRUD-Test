using Application.Customers.Queries.GetCustomer;
using Application.Customers.Queries.Models;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Test.Unit.Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQueryHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepository;
        public GetCustomerQueryHandlerTests()
        {
            _customerRepository = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task Handler_DataValid_ReturnSuccess()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var query = new GetCustomerQuery(customerId);

            var customer = CustomersTests.ReturnValid();

            _customerRepository.Setup(x => x.GetByIdAsync(customerId, default))
                .ReturnsAsync(customer);

            var queryHandler = new GetCustomerQueryHandler(_customerRepository.Object);

            // Act
            var result = await queryHandler.Handle(query, default);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.IsType<CustomerResponse>(result.Data);
        }

        [Fact]
        public async Task Handler_CustomerNotFound_ReturnFailure()
        {
            // Arrange
            var customerId = Guid.Empty;

            var query = new GetCustomerQuery(customerId);

            _customerRepository.Setup(x => x.GetByIdAsync(customerId, default))
                .ReturnsAsync((Customer?)null);

            var queryHandler = new GetCustomerQueryHandler(_customerRepository.Object);

            // Act
            var result = await queryHandler.Handle(query, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
        }
    }
}
