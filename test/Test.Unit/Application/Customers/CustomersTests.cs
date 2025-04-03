using Domain.Entities;
using Domain.Enums;

namespace Test.Unit.Application.Customers
{
    public static class CustomersTests
    {
        public static Customer ReturnValid()
        {
            return new Customer("John", "Doe", "john.doe@example.com", DocumentTypeEnum.CPF);
        }

        public static Customer ReturnInvalid()
        {
            return new Customer("", "", "", DocumentTypeEnum.CPF);
        }
    }
}
