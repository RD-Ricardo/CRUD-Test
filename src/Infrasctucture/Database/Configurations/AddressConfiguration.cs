using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrasctucture.Database.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Street)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.State)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.ZipCode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(c => c.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(c => c.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
