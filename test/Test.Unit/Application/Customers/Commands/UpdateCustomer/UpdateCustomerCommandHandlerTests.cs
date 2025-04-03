using Application.Customers.Commands.UpdateCustomer;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Test.Unit.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepository;
        public UpdateCustomerCommandHandlerTests()
        {
            _customerRepository = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task Handler_DataValid_ReturnSuccess()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var command = new UpdateCustomerCommand
            {
                Email = "teste@teste.com",
                Name = "Teste"
            };

            command.SetCustomerId(customerId);

            var customer = CustomersTests.ReturnValid();

            _customerRepository.Setup(x => x.GetByIdAsync(command.GetCustomerId(), default))
                .ReturnsAsync(customer);

            _customerRepository.Setup(x => x.Update(customer, default));

            _customerRepository.Setup(x => x.UnitOfWork.Commit());

            var commandHandler = new UpdateCustomerCommandHandler(_customerRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            _customerRepository.Verify(x => x.GetByIdAsync(command.GetCustomerId(), default), Times.Once);
            _customerRepository.Verify(x => x.Update(It.Is<Domain.Entities.Customer>(c =>
                c.Name == command.Name &&
                c.Email == command.Email
            ), default), Times.Once);
            _customerRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public async Task Handler_CustomerNotFound_ReturnFailure()
        {
            // Arrange
            var customerId = Guid.Empty;

            var command = new UpdateCustomerCommand
            {
                Email = "teste@teste.com",
                Name = "Teste"
            };

            command.SetCustomerId(customerId);

            var customer = CustomersTests.ReturnValid();

            _customerRepository.Setup(x => x.GetByIdAsync(command.GetCustomerId(), default))
                .ReturnsAsync((Customer?)null);

            _customerRepository.Setup(x => x.Update(customer, default));

            _customerRepository.Setup(x => x.UnitOfWork.Commit());

            var commandHandler = new UpdateCustomerCommandHandler(_customerRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            _customerRepository.Verify(x => x.GetByIdAsync(command.GetCustomerId(), default), Times.Once);
            _customerRepository.Verify(x => x.Update(It.Is<Domain.Entities.Customer>(c =>
                c.Name == command.Name &&
                c.Email == command.Email
            ), default), Times.Never);
            _customerRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
        }
    }
}
