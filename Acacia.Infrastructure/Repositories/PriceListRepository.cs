using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Acacia.Infrastructure.Repositories;

public class PriceListRepository : GenericRepository<PriceList>, IPriceListRepository
{
    public PriceListRepository(AcaciaDbContext context) : base(context) { }

    // Additional methods specific to PriceList can be implemented here if needed
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().AnyAsync(pl => pl.Name == name, cancellationToken);
    }
}

