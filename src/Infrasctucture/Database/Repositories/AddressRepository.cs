using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        public ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public Task<List<Address>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken)
        {
            return _context.Addresses
                .Include(a => a.Customer)
                .Where(a => a.CustomerId == customerId)
                .ToListAsync(cancellationToken);
        }
        public Task<Address?> GetByIdAsync(Guid addressId, CancellationToken cancellationToken)
        {
            return _context.Addresses
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.Id == addressId, cancellationToken);
        }

        public async Task AddAsync(Address address, CancellationToken cancellationToken)
        {
            await _context.Addresses.AddAsync(address, cancellationToken);
        }

        public void Delete(Address address, CancellationToken cancellationToken)
        {
            _context.Remove(address);
        }

        public void Update(Address address, CancellationToken cancellationToken)
        {
            _context.Update(address);
        }
    }
}
