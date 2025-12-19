using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class OilOccasionRepository : GenericRepository<OilOccasionScore>, IOilOccasionRepository
{
    public OilOccasionRepository(AcaciaDbContext context) : base(context) {}
}

