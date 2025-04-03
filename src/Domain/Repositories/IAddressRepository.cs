using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetByCustomerIdAsync(Guid customerId);
        Task AddAsync(Address address);
        void Update(Address address);
        void Delete(Address address);
        IUnitOfWork UnitOfWork { get; }
    }
}
