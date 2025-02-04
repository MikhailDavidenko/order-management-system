using OrderManagementSystem.Application.Common;
using OrderManagementSystem.Application.Exceptions;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Application.Customers;

public sealed class CustomerService : ICustomerService
{
    private readonly IUnitOfWork unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    
    public Task<IReadOnlyList<Customer>> GetAllCustomersAsync(int limit, int offset, CancellationToken cancellationToken)
        => unitOfWork.Customers.GetAllAsync(limit, offset, cancellationToken);

    public async Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
        => await unitOfWork.Customers
            .GetFirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new EntityNotFoundException($"Заказчик {id} не найден");

    public async Task<Customer> AddCustomerAsync(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(
            command.Name,
            command.Address,
            command.Discount);
        
        await unitOfWork.Customers.AddAsync(customer, cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);
        
        return customer;
    }

    public async Task<Customer> UpdateCustomerAsync(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers.GetFirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken)
                       ?? throw new EntityNotFoundException($"Заказчик {command.Id} не найден");
        
        customer.Update(command.Name, command.Code, command.Address, command.Discount);
        
        unitOfWork.Customers.Update(customer);

        await unitOfWork.CompleteAsync(cancellationToken);
        
        return customer;
    }

    public async Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers.GetFirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (customer is not null)
        {
            

            try
            {
                unitOfWork.Customers.Remove(customer);
                await unitOfWork.CompleteAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
    }
}