using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ParehNegar.Database.Database.Entities.Contents;
using ParehNegar.Domain;
using ParehNegar.Domain.BaseModels;
using ParehNegar.Logics.DatabaseLogics;
using ParehNegar.Logics.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ParehNegar.Logics.DatabaseLogics;

public class LongContractLogic<TEntity, TContract> 
    where TEntity : class, IIdSchema<long>
{
    private readonly ContractLogic<long, TEntity, TContract, TContract, TContract> _contractLogic;

    public LongContractLogic(DbContext context, IMapperProvider mapper)
    {
        _contractLogic = new(context, mapper);
    }

    public async Task<ListMessageContract<TContract>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] expressions)
    {
        return await _contractLogic.GetAllAsync(filter, expressions);
    }

    public async Task<MessageContract<TContract>> GetByAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>[] expressions)
    {
        return await _contractLogic.GetByAsync(filter, expressions);
    }

    public async Task<MessageContract<TContract>> GetByIdAsync(long id, params Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>[] expressions)
    {
        return await _contractLogic.GetByIdAsync(id, expressions);
    }

    public async Task<MessageContract<long>> AddAsync(TContract createRequest)
    {
        return await _contractLogic.AddAsync(createRequest);
    }

    public async Task<MessageContract> AddBulkAsync(IEnumerable<TContract> createRequests)
    {
        return await _contractLogic.AddBulkAsync(createRequests);
    }

    public async Task<MessageContract<TContract>> UpdateAsync(TContract updateRequest)
    {
        return await _contractLogic.UpdateAsync(updateRequest);
    }

    public async Task<MessageContract<TContract>> UpdateChangedValuesOnlyAsync(TContract updateRequest)
    {
        return await _contractLogic.UpdateChangedValuesOnlyAsync(updateRequest);
    }

    public async Task<MessageContract> UpdateBulkAsync(IEnumerable<TContract> updateRequests)
    {
        return await _contractLogic.UpdateBulkAsync(updateRequests);
    }
}
