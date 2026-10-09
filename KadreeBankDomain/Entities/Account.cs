using KadreeBankDomain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankDomain.Entities
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string AccountNumber { get; private set; } = string.Empty;
        public Guid IdCustomer { get; private set; }
        public Customer Customer { get; private set; }
        public AccountType Type { get; private set; }
        public decimal Balance { get; private set; }
        public string OriginCity { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }
        public byte[] RowVersion { get; private set; } = [];
        public ICollection<BankTransaction> Transactions { get; private set; } = new List<BankTransaction>();

        private Account()
        {
        }

        private Account(Customer customer, string accountNumber, AccountType type, string originCity)
        {
            ValidateAccount(customer, accountNumber, type, originCity);
            Id = Guid.NewGuid();
            AccountNumber = accountNumber.Trim();
            Customer = customer;
            IdCustomer = customer.Id;
            Type = type;
            Balance = 0m;
            OriginCity = originCity.Trim();
            CreatedAt = DateTime.UtcNow;
        }

        private static void ValidateAccount(Customer customer, string accountNumber, AccountType type, string originCity)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "El cliente es obligatorio");
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("El número de cuenta es obligatorio", nameof(accountNumber));
            if (string.IsNullOrWhiteSpace(originCity))
                throw new ArgumentException("La ciudad de origen es obligatoria", nameof(originCity));
            if (!Enum.IsDefined(typeof(AccountType), type))
                throw new ArgumentException("El tipo de cuenta no es válido.", nameof(type));
            bool isValidAccountType = type switch
            {
                AccountType.Ahorro =>
                    customer.Type == CustomerType.Natural,

                AccountType.Corriente =>
                    customer.Type == CustomerType.Empresa,

                _ => false
            };

            if (!isValidAccountType)
                throw new InvalidOperationException("El tipo de cuenta no corresponde al tipo de cliente.");
        }

        public static Account Open(Customer customer, string accountNumber, AccountType type, string originCity)
        {
            var account = new Account(customer, accountNumber, type, originCity);

            customer.Accounts.Add(account);

            return account;
        }

        public  BankTransaction Deposit(decimal amount, string transactionCity)
        {
            ValidateTransaction(amount, transactionCity);
            Balance += amount;
            var transaction = new BankTransaction(
                Id,
                TransactionType.Consignacion,
                amount,
                transactionCity,
                Balance);

            Transactions.Add(transaction);
            return transaction;
        }

        public BankTransaction Withdraw(decimal amount, string transactionCity)
        {
            ValidateTransaction(amount, transactionCity);

            if (amount > Balance)
                throw new InvalidOperationException("Saldo insuficiente para realizar el retiro.");

            Balance -= amount;

            var transaction = new BankTransaction(
                Id,
                TransactionType.Retiro,
                amount,
                transactionCity.Trim(),
                Balance);

            Transactions.Add(transaction);

            return transaction;
        }

        private static void ValidateTransaction(decimal amount, string transactionCity)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(amount),"El importe debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(transactionCity))
                throw new ArgumentException("La ciudad de la operación es obligatoria.",
                    nameof(transactionCity));

            if (decimal.Round(amount, 2) != amount)
                throw new ArgumentException("El importe no puede tener más de dos decimales.",
                    nameof(amount));
        }
    }
}
