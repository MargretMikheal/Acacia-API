using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class ProductTypeReposetory : GenericRepository<ProductType>, IProductTypeRepository
{
    public ProductTypeReposetory(AcaciaDbContext context) : base(context) {}
}