using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Customer customer, CancellationToken cancellationToken);
        void Update(Customer customer, CancellationToken cancellationToken);
        void Delete(Customer customer, CancellationToken cancellationToken);
        IUnitOfWork UnitOfWork { get; }
    }
}
