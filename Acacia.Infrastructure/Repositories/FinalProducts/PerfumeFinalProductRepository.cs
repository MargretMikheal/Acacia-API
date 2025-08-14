using Acacia.Core.Interfaces.IReposetories.IFinalProducts;
using Acacia.Data.Entities.FinalProducts;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories.FinalProducts;

internal class PerfumeFinalProductRepository : GenericRepository<PerfumeFinalProduct>, IPerfumeFinalProductRepository
{
    public PerfumeFinalProductRepository(AcaciaDbContext context) : base(context)
    {
    }
}
