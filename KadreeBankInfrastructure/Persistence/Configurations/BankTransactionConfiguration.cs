using KadreeBankDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KadreeBankInfrastructure.Persistence.Configurations
{
    public class BankTransactionConfiguration : IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
            builder.ToTable("BankTransactions", table =>
            {
                table.HasCheckConstraint(
                    "CK_BankTransactions_Amount_Positive",
                    "[Amount] > 0");

                table.HasCheckConstraint(
                    "CK_BankTransactions_BalanceAfter_NonNegative",
                    "[BalanceAfter] >= 0");
            });

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(t => t.BalanceAfter)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(t => t.Type)
                .IsRequired();

            builder.Property(t => t.TransactionDate)
                .IsRequired();

            builder.Property(t => t.City)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(t => new
            {
                t.AccountId,
                t.TransactionDate
            });
        }
    
    }
}
