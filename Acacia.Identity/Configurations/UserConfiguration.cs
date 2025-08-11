using Acacia.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acacia.Identity.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Configure the properties of ApplicationUser
        var hasher = new PasswordHasher<ApplicationUser>();
        // Seed data for ApplicationUser
        builder.HasData(
             new ApplicationUser
             {
                 Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                 Email = "user@acacia.com",
                 NormalizedEmail = "USER@ACACIA.COM",
                 FirstName = "System",
                 LastName = "User",
                 Address = "123 Nasser Street, Caito City, GC 12345",
                 Country = "Egypt",
                 UserName = "user@acacia",
                 NormalizedUserName = "USER@GAMEIT.COM",
                 PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                 EmailConfirmed = true
             },
             new ApplicationUser
             {
                 Id = "7e445865-a24d-4543-b6c6-9443d048cdb9",
                 Email = "admin@acacia.com",
                 NormalizedEmail = "ADMIN@ACACIA.COM",
                 FirstName = "System",
                 LastName = "Admin",
                 Address = "123 Nasser Street, Caito City, GC 12345",
                 Country = "Egypt",
                 UserName = "admin@acacia",
                 NormalizedUserName = "ADMIN@ACACIA.COM",
                 PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                 EmailConfirmed = true
             });
    }
}
