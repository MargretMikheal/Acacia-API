using Acacia.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OilConfiguration : IEntityTypeConfiguration<Oil>
{
    public void Configure(EntityTypeBuilder<Oil> builder)
    {
        builder.ToTable("Oils");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Name).HasMaxLength(200);
        builder.Property(o => o.DescriptionAr).HasMaxLength(1000);
        builder.Property(o => o.DescriptionEn).HasMaxLength(1000);

        builder.HasOne(o => o.Company)
               .WithMany(c => c.Oils)
               .HasForeignKey(o => o.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.PriceList)
               .WithMany(pl => pl.Oils)
               .HasForeignKey(o => o.PriceListId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.SeasonScore)
               .WithOne(s => s.Oil)
               .HasForeignKey<OilSeasonScore>(s => s.OilId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.OccasionScore)
               .WithOne(s => s.Oil)
               .HasForeignKey<OilOccasionScore>(s => s.OilId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
