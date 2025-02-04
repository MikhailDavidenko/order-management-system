namespace OrderManagementSystem.Domain;

/// <summary>
///     Модель Товара.
/// </summary>
public sealed class Product
{
    private Product(Guid id, string code, string name, decimal price, string category)
    {
        Id = id;
        Code = code;
        Name = name;
        Price = price;
        Category = category;
    }
    
    /// <summary>
    ///     Идентификатор товара.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    ///     Код товара.
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    ///     Наименование товара.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    ///     Цена за единицу.
    /// </summary>
    public decimal Price { get; private set; }

    /// <summary>
    ///     Категория товара.
    /// </summary>
    public string Category { get; private set; }
    
    public static Product Create(string name,decimal price, string category)
    {
        if (price <= 0)
        {
            throw new ArgumentException("Цена не может быть отрицательной или равной нулю");
        }
        
        return new(Guid.NewGuid(), GenerateProductCode(), name, price, category);
    }
    
    public void Update(string name, string code, decimal price, string category)
    {
        if (price <= 0)
        {
            throw new ArgumentException("Цена не может быть отрицательной или равной нулю");
        }

        Name = name;
        Code = code;
        Price = price;
        Category = category;
    }
    
    private static string GenerateProductCode()
    {
        Random random = new Random();

        int firstPart = random.Next(10, 100);
        int secondPart = random.Next(1000, 10000);

        char firstLetter = (char)random.Next('A', 'Z' + 1);
        char secondLetter = (char)random.Next('A', 'Z' + 1);

        int lastNumber = random.Next(0, 10);

        return $"{firstPart:D2}-{secondPart:D4}-{firstLetter}{secondLetter}{lastNumber}";
    }
}