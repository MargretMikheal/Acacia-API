using Acacia.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Acacia.Identity.Seeding;

public static class DataSeeder
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Ensure roles exist
        if (!await roleManager.RoleExistsAsync("User"))
            await roleManager.CreateAsync(new IdentityRole("User"));

        if (!await roleManager.RoleExistsAsync("Administrator"))
            await roleManager.CreateAsync(new IdentityRole("Administrator"));

        // Create default User
        if (await userManager.FindByEmailAsync("user@acacia.com") is null)
        {
            var user = new ApplicationUser
            {
                UserName = "user@acacia",
                Email = "user@acacia.com",
                NormalizedUserName = "USER@ACACIA.COM",
                NormalizedEmail = "USER@ACACIA.COM",
                FirstName = "System",
                LastName = "User",
                Address = "123 Nasser Street, Cairo City, GC 12345",
                Country = "Egypt",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, "P@ssword1");
            await userManager.AddToRoleAsync(user, "User");
        }

        // Create default Admin
        if (await userManager.FindByEmailAsync("admin@acacia.com") is null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@acacia",
                Email = "admin@acacia.com",
                NormalizedUserName = "ADMIN@ACACIA.COM",
                NormalizedEmail = "ADMIN@ACACIA.COM",
                FirstName = "System",
                LastName = "Admin",
                Address = "123 Nasser Street, Cairo City, GC 12345",
                Country = "Egypt",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, "P@ssword1");
            await userManager.AddToRoleAsync(admin, "Administrator");
        }
    }
}
