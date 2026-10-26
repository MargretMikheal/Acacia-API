using Acacia.Data.Entities.FinalProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acacia.Infrastructure.Configuration
{
    public class PerfumeFinalProductConfiguration : IEntityTypeConfiguration<PerfumeFinalProduct>
    {
        public void Configure(EntityTypeBuilder<PerfumeFinalProduct> builder)
        {
            builder.Property(p => p.AlcoholPercentage)
                   .HasPrecision(18, 2);
        }
    }
}
