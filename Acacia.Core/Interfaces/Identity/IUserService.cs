using Acacia.Core.Models.Identity;

namespace Acacia.Core.Interfaces.Identity;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User> GetUser(string userId);
}
