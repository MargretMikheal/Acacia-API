using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class OilSeasonReposetory : GenericRepository<OilSeasonScore>, IOilSeasonRepository
{
    public OilSeasonReposetory(AcaciaDbContext context) : base(context) {}
}

