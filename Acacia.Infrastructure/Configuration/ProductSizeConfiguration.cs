using Acacia.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acacia.Infrastructure.Configuration
{
    public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
    {
        public void Configure(EntityTypeBuilder<ProductSize> builder)
        {
            builder.ToTable("ProductSizes");

            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.SizeValue)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(ps => ps.SizeUnit)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasOne(ps => ps.ProductType)
                   .WithMany(pt => pt.ProductSizes)
                   .HasForeignKey(ps => ps.ProductTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ps => ps.PriceListItems)
                   .WithOne(pli => pli.ProductSize)
                   .HasForeignKey(pli => pli.ProductSizeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(ps => ps.FinalProducts)
                   .WithOne(fp => fp.ProductSize)
                   .HasForeignKey(fp => fp.ProductSizeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
