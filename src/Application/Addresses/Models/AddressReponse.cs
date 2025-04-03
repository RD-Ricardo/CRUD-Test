namespace Application.Addresses.Models
{
    public class AddressReponse
    {
        public Guid Id { get; set; }
        public string Street { get; set; } = null!;
        public string Number { get; set; } = null!;
        public string? Complement { get; set; }
        public string ZipCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
