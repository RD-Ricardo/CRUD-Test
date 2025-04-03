using CrossCutting.Utils;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<Unit>>
    {
        private readonly ICustomerRepository _customerRepository;
        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<Unit>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var existingCustomer = await _customerRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (existingCustomer is not null)
            {
                return Result<Unit>.Failure(ErrorCatalog.CustomerAlreadyExists);
            }

            var customer = new Customer(request.Name,
                request.Email,
                request.Document,
                request.DocumentType
            );

            await _customerRepository.AddAsync(customer, cancellationToken);

            await _customerRepository.UnitOfWork.Commit();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
