using Acacia.Core.Bases;
using Acacia.Core.Interfaces.Identity;
using Acacia.Core.Models.Identity;
using Acacia.Identity.Models;
using FluentValidation;
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
    private readonly ResponseHandler _responseHandler;
    private readonly IValidator<AuthRequest> _authRequestValidator;
    private readonly IValidator<RegistrationRequest> _registrationRequestValidator;

    public AuthService(IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        ResponseHandler responseHandler,
        IValidator<AuthRequest> authRequestValidator,
        IValidator<RegistrationRequest> registrationRequestValidator)
    {
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
        _userManager = userManager;
        _responseHandler = responseHandler;
        _authRequestValidator = authRequestValidator;
        _registrationRequestValidator = registrationRequestValidator;
    }

    // Handles user login and returns an authentication response containing the JWT token.
    public async Task<Response<AuthResponse>> Login(AuthRequest authRequest)
    {
        var validationResult = await _authRequestValidator.ValidateAsync(authRequest);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(f => f.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(f => f.ErrorMessage).ToList()
                );

            return _responseHandler.ValidationErrors<AuthResponse>(
                            errors: errors,
                            message: "Validation errors occurred");
        }

        var user = await _userManager.FindByEmailAsync(authRequest.Email);

        // Prevent Account Enumeration (OWASP API2:2023): return uniform error message
        if (user == null)
        {
            return _responseHandler.Unauthorized<AuthResponse>(
                        message: "Invalid email or password combination.",
                        errors: new Dictionary<string, List<string>>
                        {
                            ["Credentials"] = new List<string> { "Invalid email or password combination." }
                        });
        }

        // Enable lockout on failure to mitigate brute-force attacks
        var result = await _signInManager.CheckPasswordSignInAsync(user, authRequest.Password, lockoutOnFailure: true);
        if (result.IsLockedOut)
        {
            return _responseHandler.Unauthorized<AuthResponse>(
                        message: "Account is temporarily locked out due to multiple failed login attempts. Please try again later.",
                        errors: new Dictionary<string, List<string>>
                        {
                            ["Lockout"] = new List<string> { "Account is temporarily locked out." }
                        });
        }

        if (!result.Succeeded)
        {
            return _responseHandler.Unauthorized<AuthResponse>(
                        message: "Invalid email or password combination.",
                        errors: new Dictionary<string, List<string>>
                        {
                            ["Credentials"] = new List<string> { "Invalid email or password combination." }
                        });
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        user.LastLoginDate = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        var roles = await _userManager.GetRolesAsync(user);

        var response = new AuthResponse
        {
            User = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            },

            Token = new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            }
        };

        string responseMessage = $"User {user.UserName} logged in successfully.";
        return _responseHandler.Success<AuthResponse>(response, responseMessage);
    }

    // Handles user registration and returns a response containing the user ID.
    public async Task<Response<RegistrationResponse>> Register(RegistrationRequest registrationRequest)
    {
        var validationResult = await _registrationRequestValidator.ValidateAsync(registrationRequest);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(f => f.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(f => f.ErrorMessage).ToList()
                );

            return _responseHandler.ValidationErrors<RegistrationResponse>(
                            errors: errors,
                            message: "Validation errors occurred");
        }

        var existingUser = await _userManager.FindByEmailAsync(registrationRequest.Email);
        if (existingUser != null)
        {
            return _responseHandler.BadRequest<RegistrationResponse>(
                message: "Email address is already in use.",
                errors: new Dictionary<string, List<string>>
                {
                    ["Email"] = new List<string> { "A user with this email already exists." }
                });
        }

        var user = new ApplicationUser
        {
            UserName = registrationRequest.UserName,
            Email = registrationRequest.Email,
            FirstName = registrationRequest.FirstName,
            LastName = registrationRequest.LastName,
            Address = registrationRequest.Address,
            Country = registrationRequest.Country,
            CreatedAt = DateTime.UtcNow,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registrationRequest.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            return _responseHandler.Success(
                new RegistrationResponse { UserId = user.Id },
                "Registration successful");
        }

        var errorMessages = result.Errors.Select(e => e.Description).ToList();

        return _responseHandler.BadRequest<RegistrationResponse>(
            message: "Registration failed",
            errors: new Dictionary<string, List<string>>
            {
                ["IdentityErrors"] = errorMessages
            });
    }

    // Generates a JWT token for the authenticated user.
    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var secret = string.IsNullOrWhiteSpace(_jwtSettings.SecretKey)
            ? "default-super-secret-key-for-acacia-32-chars-minimum"
            : _jwtSettings.SecretKey;

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        // Use UTC time to avoid server local time desynchronization
        var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes > 0 ? _jwtSettings.DurationInMinutes : 60);

        var jwtSecurityToken = new JwtSecurityToken(
           issuer: _jwtSettings.Issuer,
           audience: _jwtSettings.Audience,
           claims: claims,
           expires: expires,
           signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}
