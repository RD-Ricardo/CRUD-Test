using CrossCutting.Utils;
using Domain.Repositories;
using MediatR;

namespace Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Result<Unit>>
    {
        private readonly ICustomerRepository _customerRepository;
        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<Unit>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);

            if (customer is null)
            {
                return Result<Unit>.Failure(ErrorCatalog.CustomerNotFound);
            }

            _customerRepository.Delete(customer, cancellationToken);

            await _customerRepository.UnitOfWork.Commit();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
