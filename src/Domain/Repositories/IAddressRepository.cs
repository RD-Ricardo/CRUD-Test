using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAddressRepository
    {
        Task<Address> GetAsync(Guid id);
        Task<IEnumerable<Address>> GetByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<Address>> GetAllAsync();
        Task AddAsync(Address address);
        Task UpdateAsync(Address address);
        Task DeleteAsync(Address address);
        IUnitOfWork UnitOfWork { get; }
    }
}
