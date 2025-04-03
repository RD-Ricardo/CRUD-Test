using CrossCutting.Utils;
using Domain.Repositories;
using MediatR;

namespace Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<Unit>>
    {
        private readonly ICustomerRepository _customerRepository;
        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<Unit>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.GetCustomerId(), cancellationToken);

            if (customer is null)
            {
                return Result<Unit>.Failure(ErrorCatalog.CustomerNotFound);
            }

            customer.Update(request.Name, request.Email);

            _customerRepository.Update(customer, cancellationToken);

            await _customerRepository.UnitOfWork.Commit();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
