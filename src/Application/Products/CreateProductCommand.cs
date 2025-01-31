namespace OrderManagementSystem.Application.Products;

public sealed record CreateProductCommand(
    string Name,
    string Code,
    decimal Price,
    string Category);