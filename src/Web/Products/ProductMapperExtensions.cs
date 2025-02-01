using OrderManagementSystem.Contracts.Products;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Web.Products;

public static class ProductMapperExtensions
{
    public static ProductResponse MapToProductResponse(this Product customer, decimal discount)
    {
        return new ProductResponse
        {
            Id = customer.Id,
            Name = customer.Name, 
            Code = customer.Code,
            Price = customer.Price, 
            PriceWithDiscount = customer.Price * (1 - discount / 100),
            Discount = discount,
            Category = customer.Category
        };
    }
    
}