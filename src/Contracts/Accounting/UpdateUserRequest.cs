namespace OrderManagementSystem.Contracts.Accounting;

public sealed class UpdateUserRequest
{
    public string? Name { get; set; }
    
    public string? Email { get; set; }
    
    public Guid? CustomerId { get; set; }
    
    public string? NewRoleName { get; set; }
}