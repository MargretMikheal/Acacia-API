using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acacia.Identity.Configurations;
public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        // Seed data for user roles
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                UserId = "9e224968-33e4-4652-b7b7-8574d048cdb9"
            },
            new IdentityUserRole<string>
            {
                RoleId = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                UserId = "7e445865-a24d-4543-b6c6-9443d048cdb9"
            }
        );
    }
}