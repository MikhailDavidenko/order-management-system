namespace OrderManagementSystem.Domain;

/// <summary>
///     Перечисление состояний заказа.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    ///     Новый заказ.
    /// </summary>
    New,

    /// <summary>
    ///     Заказ выполняется.
    /// </summary>
    InProgress,

    /// <summary>
    ///     Заказ выполнен.
    /// </summary>
    Completed
}