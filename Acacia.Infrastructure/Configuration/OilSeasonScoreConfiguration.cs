using Acacia.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OilSeasonScoreConfiguration : IEntityTypeConfiguration<OilSeasonScore>
{
    public void Configure(EntityTypeBuilder<OilSeasonScore> builder)
    {
        builder.ToTable("OilSeasonScores");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Summer).HasPrecision(4, 2);
        builder.Property(s => s.Winter).HasPrecision(4, 2);
        builder.Property(s => s.Fall).HasPrecision(4, 2);
        builder.Property(s => s.Spring).HasPrecision(4, 2);

        builder.HasOne(s => s.Oil)
               .WithOne(o => o.SeasonScore)
               .HasForeignKey<OilSeasonScore>(s => s.OilId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
