namespace OrderManagementSystem.Contracts.Accounting;

public sealed class UserResponse
{
    public Guid? Id { get; init; }
    
    public string? Name { get; init; }
    
    public string? Email { get; init; }
    
    public Guid? CustomerId { get; init; }
    
    public List<string> RoleNames { get; init; } = new();
}