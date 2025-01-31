using Microsoft.AspNetCore.Identity;

namespace OrderManagementSystem.Domain;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public Guid? CustomerId { get; set; }
    
    public Customer? Customer { get; set; }
}