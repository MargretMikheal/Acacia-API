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
        builder.Property(u => u.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");
    }
}
