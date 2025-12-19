using Acacia.Core.Interfaces.IReposetories;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Acacia.Infrastructure.Repositories;

public class OilRepository : GenericRepository<Oil>, IOilRepository
{
    public OilRepository(AcaciaDbContext context) : base(context) { }

    public async Task<Oil?> GetByIdWithIncludesAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Oils
            .Include(x => x.OilIngredients)
            .Include(x => x.SeasonScore)
            .Include(x => x.OccasionScore)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public async Task<List<Oil?>> GetAllWithIncludesAsync(CancellationToken cancellationToken)
    {
        return await _context.Oils
            .Include(x => x.OilIngredients)
            .Include(x => x.SeasonScore)
            .Include(x => x.OccasionScore).ToListAsync(cancellationToken);
    }
    public async Task RemoveOilIngredientsAsync(Oil oil, CancellationToken cancellationToken)
    {
        _context.Set<OilIngredient>().RemoveRange(oil.OilIngredients);
        await Task.CompletedTask;
    }

}

