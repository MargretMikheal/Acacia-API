using Acacia.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OilOccasionScoreConfiguration : IEntityTypeConfiguration<OilOccasionScore>
{
    public void Configure(EntityTypeBuilder<OilOccasionScore> builder)
    {
        builder.ToTable("OilOccasionScores");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Formal).HasPrecision(4, 2);
        builder.Property(s => s.Casual).HasPrecision(4, 2);
        builder.Property(s => s.Night).HasPrecision(4, 2);
        builder.Property(s => s.Sport).HasPrecision(4, 2);

        builder.HasOne(s => s.Oil)
               .WithOne(o => o.OccasionScore)
               .HasForeignKey<OilOccasionScore>(s => s.OilId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
