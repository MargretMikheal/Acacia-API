using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class OilOccasionReposetory : GenericRepository<OilOccasionScore>, IOilOccasionRepository
{
    public OilOccasionReposetory(AcaciaDbContext context) : base(context) {}
}

