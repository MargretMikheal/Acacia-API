using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class ProductSizeReposetory : GenericRepository<ProductSize>, IProductSizeRepository
{
    public ProductSizeReposetory(AcaciaDbContext context) : base(context) {}
}