using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class ProductSizeReposetory : GenericRepository<ProductSize>, IProductSizeRepository
{
    public ProductSizeReposetory(AcaciaDbContext context) : base(context) {}

    public async Task<bool> ExistsForProductAsync(int productTypeId, decimal size)
    {
        return _dbSet.Any(ps => ps.ProductTypeId == productTypeId && ps.SizeValue == size);
    }
}