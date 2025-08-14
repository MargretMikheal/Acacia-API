using Acacia.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acacia.Infrastructure.Configuration
{
    public class OilIngredientConfiguration : IEntityTypeConfiguration<OilIngredient>
    {
        public void Configure(EntityTypeBuilder<OilIngredient> builder)
        {
            builder.HasKey(oi => new { oi.OilId, oi.IngredientId });

            builder.HasOne(oi => oi.Oil)
                .WithMany(o => o.OilIngredients)
                .HasForeignKey(oi => oi.OilId);

            builder.HasOne(oi => oi.Ingredient)
                .WithMany(i => i.OilIngredients)
                .HasForeignKey(oi => oi.IngredientId);

            builder.Property(oi => oi.Percentage)
                   .HasPrecision(5, 2);
        }
    }
}