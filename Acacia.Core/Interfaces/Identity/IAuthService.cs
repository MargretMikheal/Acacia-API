using Acacia.Core.Bases;
using Acacia.Core.Models.Identity;

namespace Acacia.Core.Interfaces.Identity;

public interface IAuthService
{
    Task<Response<AuthResponse>> Login(AuthRequest authRequest);
    Task<Response<RegistrationResponse>> Register(RegistrationRequest registrationRequest);
}
