namespace OrderManagementSystem.Domain;

/// <summary>
///     Модель Заказчика.
/// </summary>
public sealed class Customer
{
    /// <summary>
    ///     Идентификатор заказчика.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    ///     Наименование заказчика.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Код заказчика.
    /// </summary>
    public string Code { get; }

    /// <summary>
    ///     Адрес заказчика.
    /// </summary>
    public string Address { get; }

    /// <summary>
    ///     Процент скидки для заказчика.
    /// </summary>
    public decimal? Discount { get; }
}