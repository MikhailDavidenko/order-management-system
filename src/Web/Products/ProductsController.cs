using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Products;
using OrderManagementSystem.Contracts.Products;
using OrderManagementSystem.Web.Accounting;

namespace OrderManagementSystem.Web.Products;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductService productService;

    public ProductsController(IProductService productService)
    {
        this.productService = productService;
    }
    
    [Authorize(Policy = ApplicationRoleNames.AllowAnyPolicy)]
    [HttpGet]
    public async Task<IReadOnlyList<ProductResponse>> GetProductsAsync(
        [FromQuery] int? offset,
        [FromQuery] int? limit,
        CancellationToken cancellationToken)
    {
        if (offset < 0 || limit < 1)
        {
            throw new ArgumentException($"Параметр offset не должен быть меньше нуля, limit должен быть больше нуля");
        }
        
        var products = await productService.GetAllProductsAsync(limit ?? 10, offset ?? 0, cancellationToken);
        
        var productsResponses = products
            .Select(x => x.MapToProductResponse())
            .ToList();
        
        return productsResponses;
    }
    
    [Authorize(Policy = ApplicationRoleNames.AllowAnyPolicy)]
    [HttpGet("{productId}")]
    public async Task<ProductResponse> GetProductByIdAsync(
        [FromRoute] Guid productId,
        CancellationToken cancellationToken)
    {
        var product = await productService.GetProductByIdAsync(productId, cancellationToken);

        return product.MapToProductResponse();
    }
    
    [Authorize(Roles = ApplicationRoleNames.Manager)]
    [HttpPost]
    public async Task<ProductResponse> CreateProductAsync(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand
        (
            request.Name,
            request.Code,
            request.Price.Value,
            request.Category
        );
        
        var product = await productService.AddProductAsync(command, cancellationToken);
        
        return product.MapToProductResponse();
    }
    
    [Authorize(Roles = ApplicationRoleNames.Manager)]
    [HttpPut("{productId}")]
    public async  Task<ProductResponse> UpdateProductAsync(
        [FromRoute] Guid productId,
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand
        (
            productId,
            request.Name,
            request.Code,
            request.Price.Value,
            request.Category
        );
        
        var product = await productService.UpdateProductAsync(command, cancellationToken);
        
        return product.MapToProductResponse();
    }
    
    [Authorize(Roles = ApplicationRoleNames.Manager)]
    [HttpDelete("{productId}")]
    public async Task DeleteProductAsync(
        [FromRoute] Guid productId,
        CancellationToken cancellationToken)
    {
        await productService.DeleteProductAsync(productId, cancellationToken);
    }
}