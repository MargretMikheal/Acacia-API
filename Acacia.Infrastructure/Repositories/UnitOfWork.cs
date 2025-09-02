using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.IReposetories.IFinalProducts;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.FinalProducts;

namespace Acacia.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AcaciaDbContext _context;
    public UnitOfWork(AcaciaDbContext context)
    {
        _context = context;
    }
    // Repositories
    public IBottleDesignRepository bottleDesignRepository => new BottleDesignRepository(_context);
    public ICompanyRepository companyRepository => new CompanyRepository(_context);
    public IIngredientRepository ingredientRepository => new IngredientRepository(_context);
    public IOilOccasionRepository oilOccasionRepository => new OilOccasionRepository(_context);
    public IOilRepository oilRepository => new OilRepository(_context);
    public IOilSeasonRepository oilSeasonRepository => new OilSeasonRepository(_context);
    public IPriceListItemRepository priceListItemRepository => new PriceListItemRepository(_context);
    public IPriceListRepository priceListRepository => new PriceListRepository(_context);
    public IProductSizeRepository productSizeRepository => new ProductSizeRepository(_context);
    public IProductTypeRepository productTypeRepository => new ProductTypeRepository(_context);

    // FinalProducts
    public IPerfumeFinalProductRepository perfumeFinalProductRepository => new PerfumeFinalProductRepository(_context);

    // Save changes to the database

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
