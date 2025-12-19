using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class PriceListItemRepository : GenericRepository<PriceListItem>, IPriceListItemRepository
{
    public PriceListItemRepository(AcaciaDbContext context) : base(context) {}
}

