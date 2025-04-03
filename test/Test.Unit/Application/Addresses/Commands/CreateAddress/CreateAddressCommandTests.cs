using Application.Addresses.Commands.CreateAddress;

namespace Test.Unit.Application.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandTests
    {
        [Fact]
        public void CreateAddressCommand_DataValid_ReturnValid()
        {
            // Arrange
            var command = new CreateAddressCommand
            {
                Street = "123 Main St",
                Number = "456",
                Complement = "Apt 789",
                ZipCode = "12345-6789",
                City = "Sample City",
                State = "Sample State",
                Country = "Sample Country",
                CustomerId = Guid.NewGuid()

            };

            // Act
            var validator = new CreateAddressCommandValidator().Validate(command);

            // Assert
            Assert.True(validator.IsValid);
        }

        [Fact]
        public void CreateAddressCommand_DataInvalid_ReturnInvalid()
        {
            // Arrange
            var command = new CreateAddressCommand
            {
                Street = "",
                Number = "",
                Complement = null,
                ZipCode = "1234",
                City = "",
                State = "",
                Country = ""
            };

            // Act
            var validator = new CreateAddressCommandValidator().Validate(command);

            // Assert
            Assert.False(validator.IsValid, "Validation should fail with invalid parameters.");
        }
    }
}
