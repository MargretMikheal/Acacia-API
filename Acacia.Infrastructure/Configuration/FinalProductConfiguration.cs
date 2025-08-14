using Acacia.Data.Entities;
using Acacia.Data.Entities.FinalProducts;
using Acacia.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acacia.Infrastructure.Configuration
{
    public class FinalProductConfiguration : IEntityTypeConfiguration<FinalProduct>
    {
        public void Configure(EntityTypeBuilder<FinalProduct> builder)
        {
            builder.ToTable("FinalProducts");

            builder.HasKey(fp => fp.Id);

            builder.HasOne(fp => fp.Oil)
                   .WithMany(o => o.FinalProducts)
                   .HasForeignKey(fp => fp.OilId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(fp => fp.ProductType)
                   .WithMany(pt => pt.FinalProducts)
                   .HasForeignKey(fp => fp.ProductTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(fp => fp.ProductSize)
                   .WithMany(ps => ps.FinalProducts)
                   .HasForeignKey(fp => fp.ProductSizeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(fp => fp.BottleDesign)
                   .WithMany(bd => bd.FinalProducts)
                   .HasForeignKey(fp => fp.BottleDesignId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasDiscriminator<string>("FinalProductType")
                .HasValue<PerfumeFinalProduct>(ProductTypeEnum.Perfume.ToString())
                .HasValue<BodyLotionFinalProduct>(ProductTypeEnum.BodyLotion.ToString())
                .HasValue<BodySplashFinalProduct>(ProductTypeEnum.BodySplash.ToString())
                .HasValue<CandleFinalProduct>(ProductTypeEnum.Candle.ToString())
                .HasValue<IncenseFinalProduct>(ProductTypeEnum.Incense.ToString())
                .HasValue<MukhammariaFinalProduct>(ProductTypeEnum.Mukhammaria.ToString());
        }
    }
}
