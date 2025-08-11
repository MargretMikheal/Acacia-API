using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acacia.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        // seed data for roles
        builder.HasData(
            new IdentityRole
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole
            {
                Id = "cbc43a8e-f7bb-4445-baaf-1add431ffbbf",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            }
        );
    }
}