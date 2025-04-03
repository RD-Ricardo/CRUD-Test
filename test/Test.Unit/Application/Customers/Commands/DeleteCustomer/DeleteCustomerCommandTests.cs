using Application.Customers.Commands.DeleteCustomer;

namespace Test.Unit.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandTests
    {
        [Fact]
        public void DeleteCustomerCommand_ValidData_Valid()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var command = new DeleteCustomerCommand(customerId);

            // Act
            var validator = new DeleteCustomerCommandValidator().Validate(command);

            // Assert
            Assert.NotNull(validator);
            Assert.True(validator.IsValid);
            Assert.Empty(validator.Errors);
        }

        [Fact]
        public void CreateCustomerCommand_InvalidData_ReturnErrors()
        {
            // Arrange
            var customerId = Guid.Empty;

            var command = new DeleteCustomerCommand(customerId);

            // Act
            var validator = new DeleteCustomerCommandValidator().Validate(command);

            // Assert
            Assert.NotNull(validator);
            Assert.False(validator.IsValid);
            Assert.NotEmpty(validator.Errors);
        }
    }
}
