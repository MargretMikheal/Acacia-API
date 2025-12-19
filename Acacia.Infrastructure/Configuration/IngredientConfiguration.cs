using Acacia.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acacia.Infrastructure.Configuration
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("Ingredients");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.NameAr).HasMaxLength(100);
            builder.Property(i => i.NameEn).HasMaxLength(100);
            builder.Property(i => i.ImageUrl).HasMaxLength(500);
        }
    }
}
