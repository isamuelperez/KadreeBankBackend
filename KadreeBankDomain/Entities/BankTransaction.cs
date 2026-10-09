using KadreeBankDomain.Enums;

namespace KadreeBankDomain.Entities
{
    public class BankTransaction
    {
        public Guid Id { get; private set; }

        public Guid AccountId { get; private set; }

        public Account Account { get; private set; } = null!;

        public TransactionType Type { get; private set; }

        public decimal Amount { get; private set; }

        public DateTime TransactionDate { get; private set; }

        public string City { get; private set; } = string.Empty;

        public decimal BalanceAfter { get; private set; }

        private BankTransaction()
        {
            
        }

        internal BankTransaction(
            Guid accountId,
            TransactionType type,
            decimal amount,
            string city,
            decimal balanceAfter)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Type = type;
            Amount = amount;
            City = city;
            BalanceAfter = balanceAfter;
            TransactionDate = DateTime.UtcNow;
        }

    }
}
