using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetAsync(Guid id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
        IUnitOfWork UnitOfWork { get; }
    }
}
