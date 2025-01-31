using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Application.Accounting;

public interface IUserService
{
    Task<ApplicationUser> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<ApplicationUser>> GetUsersAsync(int limit, int offset, CancellationToken cancellationToken = default);
    
    Task<ApplicationUser> RegisterUserAsync(RegisterUserCommand command, CancellationToken cancellationToken = default);
    
    Task<ApplicationUser> UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken = default);
    
    Task DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);
}