namespace OrderManagementSystem.Domain;

/// <summary>
///     Модель Товара.
/// </summary>
public sealed class Product
{
    /// <summary>
    ///     Идентификатор товара.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    ///     Код товара.
    /// </summary>
    public string Code { get; }

    /// <summary>
    ///     Наименование товара.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Цена за единицу.
    /// </summary>
    public decimal Price { get; }

    /// <summary>
    ///     Категория товара.
    /// </summary>
    public string Category { get; }
}