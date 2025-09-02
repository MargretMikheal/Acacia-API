using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class OilRepository : GenericRepository<Oil>, IOilRepository
{
    public OilRepository(AcaciaDbContext context) : base(context) {}
}

