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

        public Task<List<Address>> GetByCustomerIdAsync(Guid customerId)
        {
            return _context.Addresses.Where(a => a.CustomerId == customerId).ToListAsync();
        }

        public async Task AddAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
        }

        public void Delete(Address address)
        {
            _context.Remove(address);
        }

        public void Update(Address address)
        {
            _context.Update(address);
        }
    }
}
