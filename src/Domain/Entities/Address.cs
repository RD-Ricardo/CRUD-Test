using CrossCutting.DomainObjects;

namespace Domain.Entities
{
    public class Address : BaseEntity
    {
        public string Street { get; set; } = null!;
        public string Number { get; set; } = null!;
        public string? Complement { get; set; }
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
