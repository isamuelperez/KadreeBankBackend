using KadreeBankDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KadreeBankInfrastructure.Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts", table =>
            {
                table.HasCheckConstraint(
                    "CK_Accounts_Balance_NonNegative",
                    "[Balance] >= 0");
            });

            builder.HasKey(a => a.Id);

            builder.Property(a => a.AccountNumber)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasIndex(a => a.AccountNumber)
                .IsUnique();

            builder.Property(a => a.Type)
                .IsRequired();

            builder.Property(a => a.Balance)
            .HasPrecision(18, 2)
            .HasDefaultValue(0m)
            .IsRequired();

            builder.Property(a => a.OriginCity)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.Property(a => a.RowVersion)
                .IsRowVersion();

            builder.HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
