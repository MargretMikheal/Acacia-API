using Acacia.Core.Interfaces.IReposetories.IFinalProducts;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Acacia.Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Acacia.Infrastructure.Repositories.FinalProducts;

internal class FinalProductRepository : GenericRepository<FinalProduct>, IFinalProductRepository
{
    public FinalProductRepository(AcaciaDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<FinalProduct>> GetWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.FinalProducts
            .Include(fp => fp.Oil)
            .Include(fp => fp.ProductType)
            .Include(fp => fp.ProductSize)
            .Include(fp => fp.BottleDesign)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<FinalProduct?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.FinalProducts
            .Include(fp => fp.Oil)
            .Include(fp => fp.ProductType)
            .Include(fp => fp.ProductSize)
            .Include(fp => fp.BottleDesign)
            .FirstOrDefaultAsync(fp => fp.Id == id, cancellationToken);
    }
}
