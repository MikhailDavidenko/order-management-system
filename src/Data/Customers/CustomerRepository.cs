using OrderManagementSystem.Application.Customers;
using OrderManagementSystem.Data.Common;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Data.Customers;

internal sealed class CustomerRepository : DefaultRepository<Customer>, ICustomerRepository
{
    private readonly AppDbContext context;
    
    public CustomerRepository(AppDbContext context)
        : base(context)
    {
        this.context = context;
    }
}