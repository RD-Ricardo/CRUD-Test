using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAddressRepository
    {
        Task<Address?> GetByIdAsync(Guid addressId, CancellationToken cancellationToken);
        Task<List<Address>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken);
        Task AddAsync(Address address, CancellationToken cancellationToken);
        void Update(Address address, CancellationToken cancellationToken);
        void Delete(Address address, CancellationToken cancellationToken);
        IUnitOfWork UnitOfWork { get; }
    }
}
