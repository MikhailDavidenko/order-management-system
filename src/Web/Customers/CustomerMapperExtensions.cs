using OrderManagementSystem.Contracts.Customers;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Web.Customers;

public static class CustomerMapperExtensions
{
    public static CustomerResponse MapToCustomerResponse(this Customer customer)
    {
        return new CustomerResponse
        {
            Id = customer.Id,
            Name = customer.Name, 
            Code = customer.Code,
            Address = customer.Address, 
            Discount = customer.Discount
        };
    }
    
}