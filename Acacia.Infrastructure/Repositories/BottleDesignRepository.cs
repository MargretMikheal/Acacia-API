using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;

namespace Acacia.Infrastructure.Repositories;

public class BottleDesignRepository : GenericRepository<BottleDesign>, IBottleDesignRepository 
{
    public BottleDesignRepository(AcaciaDbContext context) : base(context) {}
}