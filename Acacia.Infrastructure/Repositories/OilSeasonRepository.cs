using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class OilSeasonRepository : GenericRepository<OilSeasonScore>, IOilSeasonRepository
{
    public OilSeasonRepository(AcaciaDbContext context) : base(context) {}
}

