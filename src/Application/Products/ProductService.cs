using OrderManagementSystem.Application.Common;
using OrderManagementSystem.Application.Exceptions;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Application.Products;

public sealed class ProductService : IProductService
{
    private readonly IUnitOfWork unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    
    public Task<IReadOnlyList<Product>> GetAllProductsAsync(int limit, int offset, CancellationToken cancellationToken)
        => unitOfWork.Products.GetAllAsync(limit, offset, cancellationToken);

    public async Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        => await unitOfWork.Products
            .GetFirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new EntityNotFoundException($"Товар {id} не найден");

    public async Task<Product> AddProductAsync(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = Product.Create(
            command.Name,
            command.Code,
            command.Price,
            command.Category);
        
        await unitOfWork.Products.AddAsync(product, cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);
        
        return product;
    }

    public async Task<Product> UpdateProductAsync(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetFirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken)
                      ?? throw new EntityNotFoundException($"Товар {command.Id} не найден");
        
        product.Update(command.Name, command.Code, command.Price, command.Category);
        
        unitOfWork.Products.Update(product);

        await unitOfWork.CompleteAsync(cancellationToken);
        
        return product;
    }

    public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetFirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (product is not null)
        {
            unitOfWork.Products.Remove(product);

            await unitOfWork.CompleteAsync(cancellationToken);
        }
        
    }
}