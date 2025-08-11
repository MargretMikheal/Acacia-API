using Acacia.Service.Models.Identity;

namespace Acacia.Service.Interfaces.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest authRequest);
    Task<RegistrationResponse> Register(RegistrationRequest registrationRequest);
}
