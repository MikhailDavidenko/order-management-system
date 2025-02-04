namespace OrderManagementSystem.Contracts.Products;

public sealed class CreateProductRequest
{
    /// <summary>
    ///     Наименование заказчика.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Цена за единицу.
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    ///     Категория товара.
    /// </summary>
    public string? Category { get; set; }
}