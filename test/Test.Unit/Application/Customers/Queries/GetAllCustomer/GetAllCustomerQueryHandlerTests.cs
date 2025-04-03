using Application.Customers.Queries.Models;
using Domain.Repositories;
using Moq;
using Test.Unit.Application.Customers;

namespace Application.Customers.Queries.GetAllCustomer
{
    public class GetAllCustomerQueryHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepository;
        public GetAllCustomerQueryHandlerTests()
        {
            _customerRepository = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task Handler_DataValid_ReturnSuccess()
        {
            // Arrange
            var query = new GetAllCustomerQuery();

            var customer = CustomersTests.ReturnValid();

            _customerRepository.Setup(x => x.GetAllAsync(default))
                .ReturnsAsync([customer]);

            var queryHandler = new GetAllCustomerQueryHandler(_customerRepository.Object);

            // Act
            var result = await queryHandler.Handle(query, default);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.IsType<List<CustomerResponse>>(result.Data);
        }
    }

}
