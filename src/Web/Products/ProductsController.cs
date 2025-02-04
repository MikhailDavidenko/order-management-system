using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Customers;
using OrderManagementSystem.Application.Exceptions;
using OrderManagementSystem.Application.Products;
using OrderManagementSystem.Contracts.Products;
using OrderManagementSystem.Domain;
using OrderManagementSystem.Web.Accounting;

namespace OrderManagementSystem.Web.Products;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductService productService;
    private readonly ICustomerService customerService;
    private readonly UserManager<ApplicationUser> userManager;

    public ProductsController(
        IProductService productService,
        ICustomerService customerService,
        UserManager<ApplicationUser> userManager)
    {
        this.productService = productService;
        this.customerService = customerService;
        this.userManager = userManager;
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
        
        var discount = await GetDiscountAsync(cancellationToken);
        
        var productsResponses = products
            .Select(x => x.MapToProductResponse(discount))
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

        var discount = await GetDiscountAsync(cancellationToken);
        
        return product.MapToProductResponse(discount);
    }
    
    [Authorize(Policy = ApplicationRoleNames.AllowAnyPolicy)]
    [HttpGet("count")]
    public async Task<int> GetTotalCountProductAsync(CancellationToken cancellationToken)
    {
        var count = await productService.GetProductsCountAsync(cancellationToken);
        
        return count;
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
            request.Price.Value,
            request.Category
        );
        
        var product = await productService.AddProductAsync(command, cancellationToken);
        
        return product.MapToProductResponse(0);
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
        
        return product.MapToProductResponse(0);
    }
    
    [Authorize(Roles = ApplicationRoleNames.Manager)]
    [HttpDelete("{productId}")]
    public async Task DeleteProductAsync(
        [FromRoute] Guid productId,
        CancellationToken cancellationToken)
    {
        await productService.DeleteProductAsync(productId, cancellationToken);
    }

    private async Task<decimal> GetDiscountAsync(CancellationToken cancellationToken)
    {
        if(User.IsInRole(ApplicationRoleNames.Customer))
        {
            var user = await userManager.GetUserAsync(User) ?? throw new EntityNotFoundException("Пользователь не найден");
            
            if (user.CustomerId == null)
            {
                throw new ArgumentException($"У пользователя с ролью Заказчик не может отсутствовать {nameof(user.CustomerId)}");
            }
            
            var customer = await customerService.GetCustomerByIdAsync(user.CustomerId.Value, cancellationToken);
            
            return customer.Discount ?? 0;
        }

        return 0;
    }
}