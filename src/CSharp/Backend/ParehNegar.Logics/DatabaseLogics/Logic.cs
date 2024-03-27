using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Logics.DatabaseLogics;

public class ExtendedDbContext<TEntity> : DbContext where TEntity : class
{
    private readonly GenericQueryBuilder<TEntity> _queryBuilder;

    public ExtendedDbContext(DbContextOptions<ExtendedDbContext<TEntity>> options) : base(options)
    {
        _queryBuilder = new GenericQueryBuilder<TEntity>(this);
    }

    public DbSet<TEntity> Entities { get; set; }

    // Delegate methods to the query builder
    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        return await _queryBuilder.GetAllAsync(filter);
    }

    public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _queryBuilder.GetByAsync(filter);
    }

    public async Task<TEntity> GetByIdAsync(object id)
    {
        return await _queryBuilder.GetByIdAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        return await _queryBuilder.AddAsync(entity);
    }

    public async Task AddBulkAsync(IEnumerable<TEntity> entities)
    {
        await _queryBuilder.AddBulkAsync(entities);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await _queryBuilder.UpdateAsync(entity);
    }

    public async Task UpdateChangedValuesOnlyAsync(TEntity entity)
    {
        await _queryBuilder.UpdateChangedValuesOnlyAsync(entity);
    }

    public async Task UpdateBulkAsync(IEnumerable<TEntity> entities)
    {
        await _queryBuilder.UpdateBulkAsync(entities);
    }
}
