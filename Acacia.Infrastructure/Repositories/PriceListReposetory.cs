using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class PriceListReposetory : GenericRepository<PriceList>, IPriceListRepository
{
    public PriceListReposetory(AcaciaDbContext context) : base(context) { }
}

