using Acacia.Core.Interfaces.IReposetories.Generic;
using Acacia.Data.Entities;

namespace Acacia.Core.Interfaces.IReposetories;

public interface IOilRepository : IGenericRepository<Oil>
{
    public Task<Oil?> GetByIdWithIncludesAsync(int id, CancellationToken cancellationToken);
    public Task<List<Oil?>> GetAllWithIncludesAsync(CancellationToken cancellationToken);
    public Task RemoveOilIngredientsAsync(Oil oil, CancellationToken cancellationToken);
}

