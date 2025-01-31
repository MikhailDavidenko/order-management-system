using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Application.Customers;

public interface ICustomerService
{
    Task<IReadOnlyList<Customer>> GetAllCustomersAsync(int limit, int offset, CancellationToken cancellationToken);
    
    Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<Customer> AddCustomerAsync(CreateCustomerCommand command, CancellationToken cancellationToken);
    
    Task<Customer> UpdateCustomerAsync(UpdateCustomerCommand command, CancellationToken cancellationToken);
    
    Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken);
}