using Acacia.Core.Models.Identity;

namespace Acacia.Core.Interfaces.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest authRequest);
    Task<RegistrationResponse> Register(RegistrationRequest registrationRequest);
}
