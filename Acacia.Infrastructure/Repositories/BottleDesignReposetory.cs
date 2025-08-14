using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class BottleDesignReposetory : GenericRepository<BottleDesign>, IBottleDesignReposetory 
{
    public BottleDesignReposetory(AcaciaDbContext context) : base(context) {}
}