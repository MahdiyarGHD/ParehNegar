using Microsoft.EntityFrameworkCore;
using ParehNegar.Domain.BaseModels;
using ParehNegar.Logics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Logics.DatabaseLogics;

public class Logic<TEntity, TId> : DbContext where TEntity : class, IIdSchema<TId>
            where TId : new()

{
    private readonly GenericQueryBuilder<TEntity, TId> _queryBuilder;
    public Logic()
        : base()
    {
        _queryBuilder = new GenericQueryBuilder<TEntity, TId>(this);
    }


    public DbSet<TEntity> Entities { get; set; }

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

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        return await _queryBuilder.UpdateAsync(entity);
    }

    public async Task<TEntity> UpdateChangedValuesOnlyAsync(TEntity entity)
    {
        return await _queryBuilder.UpdateChangedValuesOnlyAsync(entity);
    }

    public async Task UpdateBulkAsync(IEnumerable<TEntity> entities)
    {
        await _queryBuilder.UpdateBulkAsync(entities);
    }

    public async Task<TEntity> HardDeleteByIdAsync(TId id)
    {
        return await _queryBuilder.HardDeleteByIdAsync(id);
    }
    public async Task<TEntity> SoftDeleteByIdAsync(TId id)
    {
        return await _queryBuilder.SoftDeleteByIdAsync(id);
    }

    public async Task<int> BulkHardDeleteByIdAsync(IEnumerable<TId> ids)
    {
        return await _queryBuilder.BulkHardDeleteByIdAsync(ids);
    }

    public async Task<int> BulkSoftDeleteByIdAsync(IEnumerable<TId> ids)
    {
        return await _queryBuilder.BulkSoftDeleteByIdAsync(ids);
    }
}
