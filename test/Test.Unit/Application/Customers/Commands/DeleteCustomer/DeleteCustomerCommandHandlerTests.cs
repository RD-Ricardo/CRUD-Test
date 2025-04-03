using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Moq;
using Test.Unit.Application.Customers;

namespace Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepository;
        public DeleteCustomerCommandHandlerTests()
        {
            _customerRepository = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task Handler_DataValid_ReturnSuccess()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var command = new DeleteCustomerCommand(customerId);

            var customer = CustomersTests.ReturnValid();

            _customerRepository.Setup(x => x.GetByIdAsync(customerId, default))
                .ReturnsAsync(customer);

            _customerRepository.Setup(x => x.Delete(customer, default));

            _customerRepository.Setup(x => x.UnitOfWork.Commit())
                .ReturnsAsync(true);

            var commandHandler = new DeleteCustomerCommandHandler(_customerRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.IsType<Unit>(result.Data);
            _customerRepository.Verify(x => x.GetByIdAsync(customerId, default), Times.Once);
            _customerRepository.Verify(x => x.Delete(customer, default), Times.Once);
            _customerRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public async Task Handler_CustomerNotFound_ReturnFailure()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var command = new DeleteCustomerCommand(customerId);

            var customer = CustomersTests.ReturnValid();

            _customerRepository.Setup(x => x.GetByIdAsync(customerId, default))
                .ReturnsAsync((Customer?)null);

            _customerRepository.Setup(x => x.Delete(customer, default));

            _customerRepository.Setup(x => x.UnitOfWork.Commit())
                .ReturnsAsync(true);

            var commandHandler = new DeleteCustomerCommandHandler(_customerRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.IsType<Unit>(result.Data);
            _customerRepository.Verify(x => x.GetByIdAsync(customerId, default), Times.Once);
            _customerRepository.Verify(x => x.Delete(customer, default), Times.Never);
            _customerRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
        }
    }
}
