using Acacia.Data.Entities;
using Acacia.Data.Entities.FinalProducts;
using Microsoft.EntityFrameworkCore;

namespace Acacia.Infrastructure.Context
{
    public class AcaciaDbContext : DbContext
    {
        public AcaciaDbContext(DbContextOptions<AcaciaDbContext> options) : base(options) { }

        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<PriceListItem> PriceListItems { get; set; }
        public DbSet<Oil> Oils { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<OilIngredient> OilIngredients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<BottleDesign> BottleDesigns { get; set; }

        public DbSet<FinalProduct> FinalProducts { get; set; }
        public DbSet<PerfumeFinalProduct> PerfumeFinalProducts { get; set; }
        public DbSet<BodyLotionFinalProduct> BodyLotionFinalProducts { get; set; }
        public DbSet<BodySplashFinalProduct> BodySplashFinalProducts { get; set; }
        public DbSet<CandleFinalProduct> CandleFinalProducts { get; set; }
        public DbSet<IncenseFinalProduct> IncenseFinalProducts { get; set; }
        public DbSet<MukhammariaFinalProduct> MukhammariaFinalProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcaciaDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
