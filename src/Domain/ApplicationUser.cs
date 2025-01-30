using Microsoft.AspNetCore.Identity;

namespace OrderManagementSystem.Domain;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public Guid? CustomerId { get; private set; }
    
    public Customer? Customer { get; private set; }
}