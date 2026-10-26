using System.Net;
using Acacia.Core.Bases;
using Acacia.Core.Models.Identity;
using Acacia.Core.Resources;
using Acacia.Identity.Models;
using Acacia.Identity.Services;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Acacia.Tests.Identity;

public class AuthServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
    private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager;
    private readonly Mock<IValidator<AuthRequest>> _mockAuthValidator;
    private readonly Mock<IValidator<RegistrationRequest>> _mockRegValidator;
    private readonly Mock<IStringLocalizer<SharedResources>> _mockLocalizer;
    private readonly ResponseHandler _responseHandler;
    private readonly IOptions<JwtSettings> _jwtSettings;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        _mockUserManager = new Mock<UserManager<ApplicationUser>>(
            userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        var contextAccessorMock = new Mock<IHttpContextAccessor>();
        var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
            _mockUserManager.Object,
            contextAccessorMock.Object,
            claimsFactoryMock.Object,
            null!, null!, null!, null!);

        _mockAuthValidator = new Mock<IValidator<AuthRequest>>();
        _mockRegValidator = new Mock<IValidator<RegistrationRequest>>();
        _mockLocalizer = new Mock<IStringLocalizer<SharedResources>>();
        _responseHandler = new ResponseHandler(_mockLocalizer.Object);

        _jwtSettings = Options.Create(new JwtSettings
        {
            SecretKey = "SuperSecretKeyThatIsAtLeast32BytesLongForTestingSecurityPurpose!",
            Issuer = "AcaciaApi",
            Audience = "AcaciaClient",
            DurationInMinutes = 60
        });

        _authService = new AuthService(
            _jwtSettings,
            _mockSignInManager.Object,
            _mockUserManager.Object,
            _responseHandler,
            _mockAuthValidator.Object,
            _mockRegValidator.Object
        );
    }

    [Fact]
    public async Task Login_WhenUserDoesNotExist_ShouldReturnUniform401WithoutRevealingUserExistence()
    {
        // Arrange (Anti-Enumeration Protection)
        var request = new AuthRequest { Email = "nonexistent@acacia.com", Password = "Password123!" };
        _mockAuthValidator.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mockUserManager.Setup(m => m.FindByEmailAsync(request.Email))
            .ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _authService.Login(request);

        // Assert
        result.Response_Code.Should().Be(HttpStatusCode.Unauthorized);
        result.Succeeded.Should().BeFalse();
        result.Message.Should().Be("Invalid email or password combination.");
        result.Errors.Should().ContainKey("Credentials");
    }

    [Fact]
    public async Task Login_WhenPasswordIsIncorrect_ShouldEnforceLockoutAndReturnUniform401()
    {
        // Arrange
        var request = new AuthRequest { Email = "user@acacia.com", Password = "WrongPassword!" };
        var existingUser = new ApplicationUser { Id = "1", Email = request.Email, UserName = "PerfumeryUser" };

        _mockAuthValidator.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mockUserManager.Setup(m => m.FindByEmailAsync(request.Email))
            .ReturnsAsync(existingUser);

        _mockSignInManager.Setup(s => s.CheckPasswordSignInAsync(existingUser, request.Password, true))
            .ReturnsAsync(SignInResult.Failed);

        // Act
        var result = await _authService.Login(request);

        // Assert
        result.Response_Code.Should().Be(HttpStatusCode.Unauthorized);
        result.Succeeded.Should().BeFalse();
        result.Message.Should().Be("Invalid email or password combination.");
        _mockSignInManager.Verify(s => s.CheckPasswordSignInAsync(existingUser, request.Password, true), Times.Once);
    }

    [Fact]
    public async Task Login_WhenAccountIsLockedOut_ShouldReturnLockedOutMessage()
    {
        // Arrange
        var request = new AuthRequest { Email = "user@acacia.com", Password = "AnyPassword" };
        var existingUser = new ApplicationUser { Id = "1", Email = request.Email, UserName = "LockedUser" };

        _mockAuthValidator.Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _mockUserManager.Setup(m => m.FindByEmailAsync(request.Email))
            .ReturnsAsync(existingUser);

        _mockSignInManager.Setup(s => s.CheckPasswordSignInAsync(existingUser, request.Password, true))
            .ReturnsAsync(SignInResult.LockedOut);

        // Act
        var result = await _authService.Login(request);

        // Assert
        result.Response_Code.Should().Be(HttpStatusCode.Unauthorized);
        result.Succeeded.Should().BeFalse();
        result.Message.Should().Contain("locked out");
    }
}
