using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Application.Accounting;

public class UserService : IUserService
{
    
    
    public Task<ApplicationUser> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<ApplicationUser>> GetUsersAsync(int limit, int offset, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUser> RegisterUserAsync(RegisterUserCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUser> UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}