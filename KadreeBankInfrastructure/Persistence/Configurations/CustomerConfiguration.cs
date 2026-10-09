using KadreeBankDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KadreeBankInfrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.Identification)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasIndex(c => c.Identification)
                .IsUnique();

            builder.Property(c => c.Type)
                .IsRequired();

            builder.HasMany(c => c.Accounts)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.IdCustomer)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
