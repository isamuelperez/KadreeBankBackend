using KadreeBankDomain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadreeBankDomain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; } = string.Empty;
        public string Identification { get; private set; } = string.Empty;
        public CustomerType Type { get; private set; }
        public ICollection<Account> Accounts { get; private set; } = new List<Account>();
        private Customer()
        {
            
        }

        public Customer( string fullName, string identification, CustomerType type)
        {
            ValidateCustomer(fullName, identification, type);

            Id = Guid.NewGuid();
            FullName = fullName;
            Identification = identification;
            Type = type;
        }

        private static void ValidateCustomer(string fullName, string identification, CustomerType type)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("El nombre del cliente es obligatorio", nameof(fullName));
            if (string.IsNullOrWhiteSpace(identification))
                throw new ArgumentException("La identificacion es obligatoria", nameof(identification));
            if (!Enum.IsDefined(typeof(CustomerType), type))
                throw new ArgumentException("El tipo de cliente no es inválido.", nameof(type));
        }

    }
}
