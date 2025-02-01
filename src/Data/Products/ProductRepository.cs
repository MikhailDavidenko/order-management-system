using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Application.Products;
using OrderManagementSystem.Data.Common;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Data.Products;

internal sealed class ProductRepository : DefaultRepository<Product>, IProductRepository
{
    private readonly AppDbContext context;
    
    public ProductRepository(AppDbContext context)
        : base(context)
    {
        this.context = context;
    }
    
    public async Task<IReadOnlyList<Product>> GetAllWithWhereAsync(
        Expression<Func<Product, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await context.Products
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }
}