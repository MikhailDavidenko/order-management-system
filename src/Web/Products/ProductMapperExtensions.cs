using OrderManagementSystem.Contracts.Products;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Web.Products;

public static class ProductMapperExtensions
{
    public static ProductResponse MapToProductResponse(this Product customer)
    {
        return new ProductResponse
        {
            Id = customer.Id,
            Name = customer.Name, 
            Code = customer.Code,
            Price = customer.Price, 
            Category = customer.Category
        };
    }
    
}