using Acacia.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acacia.Infrastructure.Configuration
{
    public class PriceListItemConfiguration : IEntityTypeConfiguration<PriceListItem>
    {
        public void Configure(EntityTypeBuilder<PriceListItem> builder)
        {
            builder.ToTable("PriceListItems");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.HasOne(p => p.PriceList)
                   .WithMany(pl => pl.PriceListItems)
                   .HasForeignKey(p => p.PriceListId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.ProductSize)
                   .WithMany(ps => ps.PriceListItems)
                   .HasForeignKey(p => p.ProductSizeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ProductType)
               .WithMany(pt => pt.PriceListItems)
               .HasForeignKey(p => p.ProductTypeId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
