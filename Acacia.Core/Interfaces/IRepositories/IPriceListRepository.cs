using Acacia.Core.Interfaces.IReposetories.Generic;
using Acacia.Data.Entities;

namespace Acacia.Core.Interfaces.IReposetories;

public interface IPriceListRepository : IGenericRepository<PriceList>
{
    // Additional methods specific to PriceList can be added here if needed
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
}
