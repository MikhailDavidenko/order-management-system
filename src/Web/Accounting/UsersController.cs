using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Contracts.Accounting;
using OrderManagementSystem.Domain;

namespace OrderManagementSystem.Web.Accounting;

[Authorize(Roles = ApplicationRoleNames.Manager)]
[ApiController]
[Route("api/v1/[controller]")]
public sealed class UsersController : ControllerBase
{
    //ToDo: Вынести логику в сервис
    private readonly UserManager<ApplicationUser > userManager;
    private readonly RoleManager<IdentityRole<Guid>> roleManager;

    public UsersController(
        UserManager<ApplicationUser > userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] RegisterUserRequest userRequest)
    {
        if (userRequest.RoleName != null && !ApplicationRoleNames.AllRoles.Contains(userRequest.RoleName))
        {
            return BadRequest($"Роль {userRequest.RoleName} не найдена");
        }

        if (userRequest.RoleName == ApplicationRoleNames.Manager && userRequest.CustomerId == null)
        {
            return BadRequest($"У пользователя не может не быть Заказчика");
        }
        
        var user = new ApplicationUser 
        {
            UserName = userRequest.Email,
            Email = userRequest.Email,
            CustomerId = userRequest.CustomerId
        };

        var result = await userManager.CreateAsync(user, userRequest.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var userRole = userRequest.RoleName ?? ApplicationRoleNames.Customer;
        
        await userManager.AddToRoleAsync(user, userRole);
        
        var userDto = new UserResponse
        {
            Id = user.Id,
            Name = userRequest.Email,
            Email = userRequest.Email,
            CustomerId = userRequest.CustomerId,
            RoleNames = new List<string>() { userRole }
        };
        
        return Created("api/v1/users/{id}", userDto);
    }
    
    [HttpGet("{userId}")]
    public async Task<UserResponse> GetUserByIdAsync([FromRoute] Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        var roleNames = await userManager.GetRolesAsync(user);
        
        var userResponse = new UserResponse
        {
            Id = user.Id,
            Name = user.UserName,
            Email = user.Email,
            CustomerId = user.CustomerId,
            RoleNames = roleNames.ToList()
        };

        return userResponse;
    }
    
    [HttpGet]
    public async Task<IReadOnlyList<UserResponse>> GetUsersAsync(
        [FromQuery] int? offset,
        [FromQuery] int? limit,
        CancellationToken cancellationToken)
    {
        var users = await userManager.Users
            .Skip(offset ?? 0)
            .Take(limit ?? 10)
            .ToListAsync(cancellationToken);
        
        var userResponses = users
            .Select(user => new UserResponse
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                CustomerId = user.CustomerId
            })
            .ToList();
        
        return userResponses;
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUserAsync(
        [FromRoute] Guid userId,
        [FromBody] UpdateUserRequest userRequest,
        CancellationToken cancellationToken)
    {
        if (userRequest.NewRoleName != null && !ApplicationRoleNames.AllRoles.Contains(userRequest.NewRoleName))
        {
            return BadRequest($"Роль {userRequest.NewRoleName} не найдена");
        }
        
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            return NotFound($"Пользователь {userId} не найден");
        }

        user.UserName = userRequest.Email;
        user.Email = userRequest.Email;
        user.CustomerId = userRequest.CustomerId;

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        if (!String.IsNullOrEmpty(userRequest.NewRoleName))
        {
            await userManager.AddToRoleAsync(user, userRequest.NewRoleName);
        }
        
        var roleNames = await userManager.GetRolesAsync(user);
        
        var userResponse = new UserResponse
        {
            Id = user.Id,
            Name = userRequest.Email,
            Email = userRequest.Email,
            CustomerId = userRequest.CustomerId,
            RoleNames = roleNames.ToList()
        };

        return Ok(userResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser (
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(id.ToString());

        if (user != null)
        {
            await userManager.DeleteAsync(user);
        }

        return NoContent();
    }
}