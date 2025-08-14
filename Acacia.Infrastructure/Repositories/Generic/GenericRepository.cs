using Acacia.Core.Interfaces.IReposetories.Generic;
using Acacia.Data.Commons;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Acacia.Infrastructure.Repositories.Generic;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly AcaciaDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AcaciaDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        // Use AsNoTracking for better performance when not tracking changes
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        // Use AsNoTracking for better performance when not tracking changes
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
    }
    public async Task<bool> ExistsAsync(int id, CancellationToken token = default)
    {
        return await _dbSet.AsNoTracking().AnyAsync(e => e.Id == id);
    }
}
