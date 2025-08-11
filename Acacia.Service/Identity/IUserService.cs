using Acacia.Service.Models.Identity;

namespace Acacia.Service.Interfaces.Identity;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User> GetUser(string userId);
}
