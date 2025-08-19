using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Acacia.Infrastructure.Repositories;

public class ProductSizeReposetory : GenericRepository<ProductSize>, IProductSizeRepository
{
    public ProductSizeReposetory(AcaciaDbContext context) : base(context) { }

    public async Task<bool> ExistsForProductAsync(int productTypeId, decimal size, int? excludeId = null)
    {
        return await _context.ProductSizes
            .AnyAsync(x =>
                x.ProductTypeId == productTypeId &&
                x.SizeValue == size &&
                (!excludeId.HasValue || x.Id != excludeId.Value));
    }
}