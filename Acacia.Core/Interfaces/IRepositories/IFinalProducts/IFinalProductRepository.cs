using Acacia.Core.Interfaces.IReposetories.Generic;
using Acacia.Data.Entities;

namespace Acacia.Core.Interfaces.IReposetories.IFinalProducts;

public interface IFinalProductRepository : IGenericRepository<FinalProduct>
{
    Task<IEnumerable<FinalProduct>> GetWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<FinalProduct?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
}
