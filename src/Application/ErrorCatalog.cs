using CrossCutting.Utils;

namespace Application
{
    public static class ErrorCatalog
    {
        public static Error CustomerNotFound => new("Customer not found", ErrorTypeEnum.NotFound);
        public static Error CustomerAlreadyExists => new("Customer already exists", ErrorTypeEnum.Validation);
    }
}
