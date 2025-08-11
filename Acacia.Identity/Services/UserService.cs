using Acacia.Identity.Models;
using Acacia.Service.Interfaces.Identity;
using Acacia.Service.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Acacia.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _contextAccessor = contextAccessor;
    }

    // Retrieves the user ID from the current HTTP context.
    public string UserId { get => _contextAccessor.HttpContext?.User?.FindFirstValue("uid"); }

    // Retrieves the user ID from the current HTTP context as a Claim.
    public async Task<User> GetUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return new User
        {
            Id = user.Id,
            Email = user.Email,
            Firstname = user.FirstName,
            Lastname = user.LastName
        };
    }

    // Retrieves a list of users in the "User" role.
    public async Task<List<User>> GetUsers()
    {
        var employees = await _userManager.GetUsersInRoleAsync("User");

        return employees.Select(q => new User
        {
            Id = q.Id,
            Email = q.Email,
            Firstname = q.FirstName,
            Lastname = q.LastName
        }).ToList();
    }
}
