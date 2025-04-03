using Application;
using Application.Customers.Commands.CreateCustomer;
using Domain.Repositories;
using Moq;

namespace Test.Unit.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepository;
        public CreateCustomerCommandHandlerTests()
        {
            _customerRepository = new Mock<ICustomerRepository>();
        }

        [Fact]
        public async Task Handler_DataValid_ReturnSuccess()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                Document = Domain.Enums.DocumentTypeEnum.CPF.ToString(),
                DocumentType = Domain.Enums.DocumentTypeEnum.CPF,
                Email = "teste@teste.com",
                Name = "Teste"
            };

            var customer = CustomersTests.ReturnValid();

            _customerRepository.Setup(x => x.GetByEmailAsync(command.Email, default))
                .ReturnsAsync((Domain.Entities.Customer?)null);

            _customerRepository.Setup(x => x.AddAsync(customer, default));

            _customerRepository.Setup(x => x.UnitOfWork.Commit())
                .ReturnsAsync(true);

            var commandHandler = new CreateCustomerCommandHandler(_customerRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, default);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            _customerRepository.Verify(x => x.GetByEmailAsync(command.Email, default), Times.Once);
            _customerRepository.Verify(x => x.AddAsync(It.Is<Domain.Entities.Customer>(c =>
                c.Name == command.Name &&
                c.Email == command.Email &&
                c.Document == command.Document
            ), default), Times.Once);
            _customerRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public async Task Handler_CustomerAlreadyExists_ReturnFailure()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                Document = Domain.Enums.DocumentTypeEnum.CPF.ToString(),
                DocumentType = Domain.Enums.DocumentTypeEnum.CPF,
                Email = "teste@teste.com",
                Name = "Teste"
            };

            var customer = CustomersTests.ReturnValid();

            _customerRepository.Setup(x => x.GetByEmailAsync(command.Email, default))
                .ReturnsAsync(customer);

            _customerRepository.Setup(x => x.AddAsync(customer, default));

            _customerRepository.Setup(x => x.UnitOfWork.Commit())
                .ReturnsAsync(false);

            var commandHandler = new CreateCustomerCommandHandler(_customerRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, default);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Errors);
            Assert.Contains(result.Errors, x => x.Message == ErrorCatalog.CustomerAlreadyExists.Message);
            _customerRepository.Verify(x => x.GetByEmailAsync(command.Email, default), Times.Once);
            _customerRepository.Verify(x => x.AddAsync(It.IsAny<Domain.Entities.Customer>(), default), Times.Never);
            _customerRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
        }
    }
}
