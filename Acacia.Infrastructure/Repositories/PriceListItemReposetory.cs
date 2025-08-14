using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class PriceListItemReposetory : GenericRepository<PriceListItem>, IPriceListItemRepository
{
    public PriceListItemReposetory(AcaciaDbContext context) : base(context) {}
}

