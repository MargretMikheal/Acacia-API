using Acacia.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Acacia.Infrastructure.Configuration
{


    public class PriceListConfiguration : IEntityTypeConfiguration<PriceList>
    {
        public void Configure(EntityTypeBuilder<PriceList> builder)
        {
            builder.ToTable("PriceLists");

            builder.HasKey(pl => pl.Id);

            builder.Property(pl => pl.Name)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(pl => pl.Description)
                   .HasMaxLength(1000);

            builder.HasMany(pl => pl.Oils)
                   .WithOne(o => o.PriceList)
                   .HasForeignKey(o => o.PriceListId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(pl => pl.PriceListItems)
                   .WithOne(item => item.PriceList)
                   .HasForeignKey(item => item.PriceListId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
