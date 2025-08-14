using Acacia.Data.Entities;
using Acacia.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acacia.Infrastructure.Configuration
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.ToTable("ProductTypes");

            builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.NameAr).HasMaxLength(100).IsRequired();
            builder.Property(pt => pt.NameEn).HasMaxLength(100).IsRequired();

            builder.HasMany(pt => pt.ProductSizes)
               .WithOne(ps => ps.ProductType)
               .HasForeignKey(ps => ps.ProductTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(pt => pt.PriceListItems)
                   .WithOne(pli => pli.ProductType)
                   .HasForeignKey(pli => pli.ProductTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(pt => pt.BottleDesigns)
                   .WithOne(bd => bd.ProductType)
                   .HasForeignKey(bd => bd.ProductTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(pt => pt.FinalProducts)
                   .WithOne(fp => fp.ProductType)
                   .HasForeignKey(fp => fp.ProductTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new ProductType { Id = (int)ProductTypeEnum.Perfume, NameAr = "عطر", NameEn = ProductTypeEnum.Perfume.ToString() },
                new ProductType { Id = (int)ProductTypeEnum.Mukhammaria, NameAr = "مخمريه", NameEn = ProductTypeEnum.Mukhammaria.ToString() },
                new ProductType { Id = (int)ProductTypeEnum.BodySplash, NameAr = "بودي سبلاش", NameEn = ProductTypeEnum.BodySplash.ToString() },
                new ProductType { Id = (int)ProductTypeEnum.Candle, NameAr = "شموع", NameEn = ProductTypeEnum.Candle.ToString() },
                new ProductType { Id = (int)ProductTypeEnum.BodyLotion, NameAr = "لوشن الجسم", NameEn = ProductTypeEnum.BodyLotion.ToString() },
                new ProductType { Id = (int)ProductTypeEnum.Incense, NameAr = "مباخر", NameEn = ProductTypeEnum.Incense.ToString() }
            );
        }
    }
}
