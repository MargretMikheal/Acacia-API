using Acacia.Api.ApiBases;
using Acacia.Core.Interfaces.Identity;
using Acacia.Core.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Acacia.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : AppControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authenticationService)
    {
        _authService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
    {
        var response = await _authService.Login(request);
        return NewResult(response);
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrationRequest request)
    {
        var response = await _authService.Register(request);
        return NewResult(response);
    }
}
