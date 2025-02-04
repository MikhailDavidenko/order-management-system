namespace OrderManagementSystem.Contracts.Customers;

public sealed class CreateCustomerRequest
{
    /// <summary>
    ///     Наименование заказчика.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Адрес заказчика.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    ///     Процент скидки для заказчика.
    /// </summary>
    public decimal? Discount { get; set; }
}