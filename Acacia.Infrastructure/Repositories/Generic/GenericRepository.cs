using Acacia.Core.Interfaces.IReposetories.Generic;
using Acacia.Data.Commons;
using Acacia.Data.Entities;
using Acacia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading;

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
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        // Use AsNoTracking for better performance when not tracking changes
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        return true;
    }
    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(e => e.Id == id, cancellationToken);
    }
}
