using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class OilReposetory : GenericRepository<Oil>, IOilReposetory
{
    public OilReposetory(AcaciaDbContext context) : base(context) {}
}

