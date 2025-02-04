namespace OrderManagementSystem.Application.Products;

public sealed record CreateProductCommand(
    string Name,
    decimal Price,
    string Category);