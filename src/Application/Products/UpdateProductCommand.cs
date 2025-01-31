namespace OrderManagementSystem.Application.Products;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    string Code,
    decimal Price,
    string Category);