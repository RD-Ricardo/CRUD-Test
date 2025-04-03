using Application.Customers.Commands.CreateCustomer;
using Domain.Enums;

namespace Test.Unit.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandTests
    {
        [Fact]
        public void CreateCustomerCommand_ValidData_Valid()
        {

            // Arrange
            var command = new CreateCustomerCommand
            {
                Document = Domain.Enums.DocumentTypeEnum.CPF.ToString(),
                DocumentType = Domain.Enums.DocumentTypeEnum.CPF,
                Email = "teste@teste.com",
                Name = "Teste"
            };

            // Act
            var validator = new CreateCustomerCommandValidator().Validate(command);

            // Assert
            Assert.NotNull(validator);
            Assert.True(validator.IsValid);
            Assert.Empty(validator.Errors);
        }

        [Fact]
        public void CreateCustomerCommand_InvalidData_ReturnErrors()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                Name = string.Empty,
                Document = string.Empty,
            };

            // Act
            var validator = new CreateCustomerCommandValidator().Validate(command);

            // Assert
            Assert.NotNull(validator);
            Assert.False(validator.IsValid);
            Assert.NotEmpty(validator.Errors);
            Assert.Contains(validator.Errors, x => x.PropertyName == nameof(command.Name));
            Assert.Contains(validator.Errors, x => x.PropertyName == nameof(command.Document));
        }


        [Fact]
        public void CreateCustomerCommand_InvalidDocumentType_ReturnError()
        {
            // Arrange
            var command = new CreateCustomerCommand
            {
                Name = "Test name",
                Document = "Document teste",
                Email = "teste@teste.com",
                DocumentType = (DocumentTypeEnum)6
            };

            // Act
            var validator = new CreateCustomerCommandValidator().Validate(command);

            // Assert
            Assert.NotNull(validator);
            Assert.False(validator.IsValid);
            Assert.NotEmpty(validator.Errors);
            Assert.Contains(validator.Errors, x => x.PropertyName == nameof(command.DocumentType));
        }
    }
}
