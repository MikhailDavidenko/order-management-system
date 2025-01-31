namespace OrderManagementSystem.Domain;

/// <summary>
///     Модель Заказчика.
/// </summary>
public sealed class Customer
{
    private Customer(Guid id, string name, string code, string address, decimal? discount)
    {
        Id = id;
        Name = name;
        Code = code;
        Address = address;
        Discount = discount;
    }
    
    /// <summary>
    ///     Идентификатор заказчика.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    ///     Наименование заказчика.
    /// </summary>
    public string Name { get; private set;  }

    /// <summary>
    ///     Код заказчика.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    ///     Адрес заказчика.
    /// </summary>
    public string Address { get; private set; }

    /// <summary>
    ///     Процент скидки для заказчика.
    /// </summary>
    public decimal? Discount { get; private set; }

    public static Customer Create(string name, string code, string address, decimal? discount)
    {
        if (discount != null && discount < 0)
        {
            throw new ArgumentException("Скидка не может быть отрицательной");
        }

        return new(Guid.NewGuid(), name, code, address, discount);
    }
    
    public void Update(string name, string code, string address, decimal? discount)
    {
        if (discount != null && discount < 0)
        {
            throw new ArgumentException("Скидка не может быть отрицательной");
        }

        Name = name;
        Code = code;
        Address = address;
        Discount = discount;
    }
}