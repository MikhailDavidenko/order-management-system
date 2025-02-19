namespace OrderManagementSystem.Web.Accounting;

public static class ApplicationRoleNames
{
    public const string Manager = "Менеджер";
    public const string Customer = "Заказчик";
    
    public const string AllowAnyPolicy = "AllowAnyAuthorized";

    public static IEnumerable<string> AllRoles
    {
        get
        {
            yield return Manager;
            yield return Customer;
        }
    }
}