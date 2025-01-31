namespace OrderManagementSystem.Contracts.Accounting;

public sealed class RegisterUserRequest
{
    public string? Email { get; set; }
    
    public string? Password { get; set; }
    
    public Guid? CustomerId { get; set; }
    
    public string? RoleName { get; set; }
}