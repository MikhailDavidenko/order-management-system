using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Application.Products;

public interface IProductService
{
    Task<int> GetProductsCountAsync(CancellationToken cancellationToken);
    
    Task<IReadOnlyList<Product>> GetAllProductsAsync(int limit, int offset, CancellationToken cancellationToken);
    
    Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<Product> AddProductAsync(CreateProductCommand command, CancellationToken cancellationToken);
    
    Task<Product> UpdateProductAsync(UpdateProductCommand command, CancellationToken cancellationToken);
    
    Task DeleteProductAsync(Guid id, CancellationToken cancellationToken);
}