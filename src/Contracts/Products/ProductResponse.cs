namespace OrderManagementSystem.Contracts.Products;

public sealed class ProductResponse
{
    /// <summary>
    ///     Идентификатор заказчика.
    /// </summary>
    public Guid? Id { get; init; }

    /// <summary>
    ///     Наименование заказчика.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    ///     Код заказчика.
    /// </summary>
    public string? Code { get; init; }

    /// <summary>
    ///     Цена за единицу.
    /// </summary>
    public decimal? Price { get; init; }
    
    /// <summary>
    ///     Цена за единицу с учетом скидки.
    /// </summary>
    public decimal? PriceWithDiscount { get; init; }
    
    /// <summary>
    ///     Скидка.
    /// </summary>
    public decimal? Discount { get; init; }

    /// <summary>
    ///     Категория товара.
    /// </summary>
    public string? Category { get; init; }
}