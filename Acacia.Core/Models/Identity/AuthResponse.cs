namespace Acacia.Core.Models.Identity;

public class UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class TokenDto
{
    public string Token { get; set; }
}

public class AuthResponse
{
    public UserDto User { get; set; }
    public TokenDto Token { get; set; }
}
