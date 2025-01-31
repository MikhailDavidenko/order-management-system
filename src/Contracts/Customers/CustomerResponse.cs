namespace OrderManagementSystem.Contracts.Customers;

public sealed class CustomerResponse
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
    ///     Адрес заказчика.
    /// </summary>
    public string? Address { get; init; }

    /// <summary>
    ///     Процент скидки для заказчика.
    /// </summary>
    public decimal? Discount { get; init; }
}