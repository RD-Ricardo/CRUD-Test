using CrossCutting.Utils;

namespace Application
{
    public static class ErrorCatalog
    {
        public static Error CustomerNotFound => new("Customer not found", ErrorTypeEnum.NotFound);
        public static Error CustomerAlreadyExists => new("Customer already exists", ErrorTypeEnum.Validation);
        public static Error AddressNotFound => new("Address not found", ErrorTypeEnum.NotFound);
        public static Error NoAddressesFound => new("No addresses found for this customer.", ErrorTypeEnum.NotFound);
    }
}
