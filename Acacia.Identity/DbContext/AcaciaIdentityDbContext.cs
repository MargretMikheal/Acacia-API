using Acacia.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Acacia.Identity.DbContext;

internal class AcaciaIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public AcaciaIdentityDbContext(DbContextOptions<AcaciaIdentityDbContext> options)
        : base(options) {}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AcaciaIdentityDbContext).Assembly);
    }
}

