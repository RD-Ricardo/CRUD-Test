using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrasctucture.Database.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddAsync(Customer customer, CancellationToken cancellationToken)
        {
            await _context.Customers.AddAsync(customer, cancellationToken);
        }

        public void Delete(Customer customer, CancellationToken cancellationToken)
        {
            _context.Customers.Remove(customer);
        }

        public Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken)
        {
            return _context.Customers.ToListAsync(cancellationToken);
        }

        public Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return _context.Customers.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context.Customers.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public void Update(Customer customer, CancellationToken cancellationToken)
        {
            _context.Customers.Update(customer);
        }
    }
}
