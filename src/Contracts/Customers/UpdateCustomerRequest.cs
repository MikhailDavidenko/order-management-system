namespace OrderManagementSystem.Contracts.Customers;

public sealed class UpdateCustomerRequest
{
    /// <summary>
    ///     Наименование заказчика.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Код заказчика.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    ///     Адрес заказчика.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    ///     Процент скидки для заказчика.
    /// </summary>
    public decimal? Discount { get; set; }
}