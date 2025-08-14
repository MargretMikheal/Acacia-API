using Acacia.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acacia.Infrastructure.Configuration
{
    public class BottleDesignConfiguration : IEntityTypeConfiguration<BottleDesign>
    {
        public void Configure(EntityTypeBuilder<BottleDesign> builder)
        {
            builder.ToTable("BottleDesigns");

            builder.HasKey(bd => bd.Id);


            builder.HasOne(bd => bd.ProductType)
                   .WithMany(pt => pt.BottleDesigns)
                   .HasForeignKey(bd => bd.ProductTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
