using KadreeBankApplication.Interfaces.Repositories;
using KadreeBankDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KadreeBankInfrastructure.Persistence.Respositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly KadreeBankDbContext _context;

        public CustomerRepository(KadreeBankDbContext contex)
        {
            _context = contex;
        }
        public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            await _context.Customers.AddAsync(customer, cancellationToken);
        }

        public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Customer?> GetByIdentificationAsync(string identification, CancellationToken cancellationToken = default)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Identification == identification, cancellationToken);
        }
    }
}
