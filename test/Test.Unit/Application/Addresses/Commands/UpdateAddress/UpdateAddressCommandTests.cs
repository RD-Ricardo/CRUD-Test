using Application.Addresses.Commands.UpdateAddress;

namespace Test.Unit.Application.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandTests
    {
        [Fact]
        public void UpdateAddressCommand_DataValid_ReturnValid()
        {
            // Arrange
            var addressId = Guid.NewGuid();

            var command = new UpdateAddressCommand
            {
                Street = "123 Main St",
                Number = "456",
                Complement = "Apt 789",
                ZipCode = "12345-6789",
                City = "Sample City",
                State = "Sample State",
                Country = "Sample Country"
            };

            command.SetAddressId(addressId);

            // Act
            var validator = new UpdateAddressCommandValidator().Validate(command);


            // Assert
            Assert.True(validator.IsValid, "Validation should succeed with valid parameters.");
        }

        [Fact]
        public void UpdateAddressCommand_DataInvalid_ReturnInvalid()
        {
            // Arrange
            var command = new UpdateAddressCommand
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
            var validator = new UpdateAddressCommandValidator().Validate(command);

            // Assert
            Assert.False(validator.IsValid, "Validation should fail with invalid parameters.");
        }
    }
}
