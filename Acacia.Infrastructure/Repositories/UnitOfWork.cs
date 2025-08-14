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
    public IBottleDesignReposetory bottleDesignReposetory => new BottleDesignReposetory(_context);
    public ICompanyReposetory companyReposetory => new CompanyReposetory(_context);
    public IIngredientRepository ingredientRepository => new IngredientReposetory(_context);
    public IOilOccasionRepository oilOccasionRepository => new OilOccasionReposetory(_context);
    public IOilReposetory oilReposetory => new OilReposetory(_context);
    public IOilSeasonRepository oilSeasonRepository => new OilSeasonReposetory(_context);
    public IPriceListItemRepository priceListItemRepository => new PriceListItemReposetory(_context);
    public IPriceListRepository priceListRepository => new PriceListReposetory(_context);
    public IProductSizeRepository productSizeRepository => new ProductSizeReposetory(_context);
    public IProductTypeRepository productTypeRepository => new ProductTypeReposetory(_context);

    // FinalProducts
    public IPerfumeFinalProductRepository perfumeFinalProductRepository => new PerfumeFinalProductRepository(_context);

    // Save changes to the database

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
