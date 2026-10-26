using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.IReposetories.IFinalProducts;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.FinalProducts;

namespace Acacia.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AcaciaDbContext _context;

    private IBottleDesignRepository? _bottleDesignRepository;
    private ICompanyRepository? _companyRepository;
    private IIngredientRepository? _ingredientRepository;
    private IOilOccasionRepository? _oilOccasionRepository;
    private IOilRepository? _oilRepository;
    private IOilSeasonRepository? _oilSeasonRepository;
    private IPriceListItemRepository? _priceListItemRepository;
    private IPriceListRepository? _priceListRepository;
    private IProductSizeRepository? _productSizeRepository;
    private IProductTypeRepository? _productTypeRepository;
    private IFinalProductRepository? _finalProductRepository;
    private IPerfumeFinalProductRepository? _perfumeFinalProductRepository;

    public UnitOfWork(AcaciaDbContext context)
    {
        _context = context;
    }

    // Repositories cached with backing fields to prevent excessive ephemeral allocations
    public IBottleDesignRepository bottleDesignRepository => _bottleDesignRepository ??= new BottleDesignRepository(_context);
    public ICompanyRepository companyRepository => _companyRepository ??= new CompanyRepository(_context);
    public IIngredientRepository ingredientRepository => _ingredientRepository ??= new IngredientRepository(_context);
    public IOilOccasionRepository oilOccasionRepository => _oilOccasionRepository ??= new OilOccasionRepository(_context);
    public IOilRepository oilRepository => _oilRepository ??= new OilRepository(_context);
    public IOilSeasonRepository oilSeasonRepository => _oilSeasonRepository ??= new OilSeasonRepository(_context);
    public IPriceListItemRepository priceListItemRepository => _priceListItemRepository ??= new PriceListItemRepository(_context);
    public IPriceListRepository priceListRepository => _priceListRepository ??= new PriceListRepository(_context);
    public IProductSizeRepository productSizeRepository => _productSizeRepository ??= new ProductSizeRepository(_context);
    public IProductTypeRepository productTypeRepository => _productTypeRepository ??= new ProductTypeRepository(_context);

    // FinalProducts
    public IFinalProductRepository finalProductRepository => _finalProductRepository ??= new FinalProductRepository(_context);
    public IPerfumeFinalProductRepository perfumeFinalProductRepository => _perfumeFinalProductRepository ??= new PerfumeFinalProductRepository(_context);

    // Save changes to the database
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
