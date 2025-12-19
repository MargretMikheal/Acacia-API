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
                    new ProductType { Id = (int)ProductTypeEnum.Perfume, NameAr = "عطر", NameEn = nameof(ProductTypeEnum.Perfume), CreationDate = new DateTime(2024, 01, 01) },
                    new ProductType { Id = (int)ProductTypeEnum.Mukhammaria, NameAr = "مخمريه", NameEn = nameof(ProductTypeEnum.Mukhammaria), CreationDate = new DateTime(2024, 01, 01) },
                    new ProductType { Id = (int)ProductTypeEnum.BodySplash, NameAr = "بودي سبلاش", NameEn = nameof(ProductTypeEnum.BodySplash), CreationDate = new DateTime(2024, 01, 01) },
                    new ProductType { Id = (int)ProductTypeEnum.Candle, NameAr = "شموع", NameEn = nameof(ProductTypeEnum.Candle), CreationDate = new DateTime(2024, 01, 01) },
                    new ProductType { Id = (int)ProductTypeEnum.BodyLotion, NameAr = "لوشن الجسم", NameEn = nameof(ProductTypeEnum.BodyLotion), CreationDate = new DateTime(2024, 01, 01) },
                    new ProductType { Id = (int)ProductTypeEnum.Incense, NameAr = "مباخر", NameEn = nameof(ProductTypeEnum.Incense), CreationDate = new DateTime(2024, 01, 01) }
                );

        }
    }
}
