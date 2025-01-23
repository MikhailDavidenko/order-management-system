namespace OrderManagementSystem.Data.Common;

public sealed class DbConnectionOptions
{
    public const string OptionsKey = "ConnectionStrings";

    public string? Oms { get; set; }

    public string RequiredConnectionString => Oms ?? throw new ArgumentNullException(EmptyConnectionStringMessage);

    private const string EmptyConnectionStringMessage = $"Конфигурационное значение «{nameof(Oms)}» не задано";
}