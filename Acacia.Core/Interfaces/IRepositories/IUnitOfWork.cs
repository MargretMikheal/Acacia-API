using Acacia.Core.Interfaces.IReposetories.IFinalProducts;

namespace Acacia.Core.Interfaces.IReposetories;

public interface IUnitOfWork
{
    // Repositories
    IBottleDesignRepository bottleDesignRepository { get; }
    ICompanyRepository companyRepository { get; }
    IIngredientRepository ingredientRepository { get; }
    IOilOccasionRepository oilOccasionRepository { get; }
    IOilRepository oilRepository { get; }
    IOilSeasonRepository oilSeasonRepository { get; }
    IPriceListItemRepository priceListItemRepository { get; }
    IPriceListRepository priceListRepository { get; }
    IProductSizeRepository productSizeRepository { get; }   
    IProductTypeRepository productTypeRepository { get; }

    // FinalProducts
    IPerfumeFinalProductRepository perfumeFinalProductRepository { get; }

    // Save changes to the database
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
