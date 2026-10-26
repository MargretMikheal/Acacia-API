using System;
using System.IO;
using System.Threading.Tasks;
using Acacia.Identity;
using Acacia.Identity.Models;
using Acacia.Identity.Seeding;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Acacia.Tests.Identity;

public class AdminSeederIntegrationTests
{
    private readonly ITestOutputHelper _output;

    public AdminSeederIntegrationTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task SeedAdminAccount_SuccessfullySeedsAdminAndUser()
    {
        var configPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../../Acacia-API/Acacia.Api/appsettings.json"));
        if (!File.Exists(configPath))
        {
            configPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
        }

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(configPath, optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("AcaciaDbContext");
        connectionString.Should().NotBeNullOrWhiteSpace("Connection string must be present in appsettings.json");

        var services = new ServiceCollection();
        services.AddLogging(builder => builder.AddConsole());
        services.AddIdentityServices(configuration);

        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Execute Seeder
        await DataSeeder.SeedAsync(userManager, roleManager);

        // Verify Administrator Account
        var admin = await userManager.FindByEmailAsync("admin@acacia.com");
        admin.Should().NotBeNull("Admin account must exist in the database");
        admin!.Email.Should().Be("admin@acacia.com");
        admin.EmailConfirmed.Should().BeTrue();

        var isAdmin = await userManager.IsInRoleAsync(admin, "Administrator");
        isAdmin.Should().BeTrue("Admin account must be assigned the 'Administrator' role");

        var adminPasswordValid = await userManager.CheckPasswordAsync(admin, "P@ssword1");
        adminPasswordValid.Should().BeTrue("Admin password must match default credentials 'P@ssword1'");

        _output.WriteLine($"[SUCCESS] Admin Account Verified: ID={admin.Id}, Email={admin.Email}, Role=Administrator");

        // Verify Standard User Account
        var user = await userManager.FindByEmailAsync("user@acacia.com");
        user.Should().NotBeNull("Default user account must exist in the database");
        user!.Email.Should().Be("user@acacia.com");

        var isUser = await userManager.IsInRoleAsync(user, "User");
        isUser.Should().BeTrue("User account must be assigned the 'User' role");

        var userPasswordValid = await userManager.CheckPasswordAsync(user, "P@ssword1");
        userPasswordValid.Should().BeTrue("User password must match default credentials 'P@ssword1'");

        _output.WriteLine($"[SUCCESS] Standard User Account Verified: ID={user.Id}, Email={user.Email}, Role=User");
    }
}
