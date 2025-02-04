using System.Linq.Expressions;
using OrderManagementSystem.Application.Common;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Application.Products;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyList<Product>> GetAllWithWhereAsync(
        Expression<Func<Product, bool>> predicate,
        CancellationToken cancellationToken);

    Task<int> GetProductsCountAsync(CancellationToken cancellationToken);
}