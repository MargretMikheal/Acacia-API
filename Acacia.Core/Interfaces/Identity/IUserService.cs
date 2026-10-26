using Acacia.Core.Models.Identity;

namespace Acacia.Core.Interfaces.Identity;

public interface IUserService
{
    string UserId { get; }
    Task<UserDto> GetUser(string userId);
    Task<List<UserDto>> GetUsers();
}
