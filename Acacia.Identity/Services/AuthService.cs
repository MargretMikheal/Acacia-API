using Acacia.Core.Exeptions;
using Acacia.Identity.Models;
using Acacia.Service.Interfaces.Identity;
using Acacia.Service.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Acacia.Identity.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
        {
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    // Handles user login and returns an authentication response containing the JWT token.
    public async Task<AuthResponse> Login(AuthRequest authRequest)
    {
        var user = await _userManager.FindByEmailAsync(authRequest.Email);
        if (user == null)
        {
            throw new NotFoundException($"User with {authRequest.Email} not found.", authRequest.Email);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, authRequest.Password, false);
        if (result.Succeeded == false)
        {
            throw new BadRequestException($"Credentials for '{authRequest.Email} aren't valid'.");
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        user.LastLoginDate = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        var response = new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };

        return response;
    }

    // Handles user registration and returns a response containing the user ID.
    public async Task<RegistrationResponse> Register(RegistrationRequest registrationRequest)
    {
        var user = new ApplicationUser
        {
            UserName = registrationRequest.UserName,
            Email = registrationRequest.Email,
            FirstName = registrationRequest.FirstName,
            LastName = registrationRequest.LastName,
            Address = registrationRequest.Address,
            Country = registrationRequest.Country,
            EmailConfirmed = true
        };

        var result = _userManager.CreateAsync(user, registrationRequest.Password);

        if (result.Result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Employee");
            return new RegistrationResponse() { UserId = user.Id };
        }
        else
        {
            StringBuilder str = new StringBuilder();
            foreach (var err in result.Result.Errors)
            {
                str.AppendFormat("•{0}\n", err.Description);
            }

            throw new BadRequestException($"{str}");
        }
    }

    // Generates a JWT token for the authenticated user.
    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
           issuer: _jwtSettings.Issuer,
           audience: _jwtSettings.Audience,
           claims: claims,
           expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
           signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }

}
