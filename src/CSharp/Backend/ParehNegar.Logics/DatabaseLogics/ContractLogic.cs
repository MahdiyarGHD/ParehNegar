using EasyMicroservices.Mapper.Interfaces;
using Microsoft.EntityFrameworkCore;
using ParehNegar.Logics.DatabaseLogics;
using ParehNegar.Logics.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ParehNegar.Logics.DatabaseLogics;

public class ContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract>
        : IContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract>
    where TEntity : class
{
    private readonly GenericQueryBuilder<TEntity> _queryBuilder;
    private readonly IMapperProvider _mapper;

    public ContractLogic(DbContext context, IMapperProvider mapper)
    {
        _queryBuilder = new(context);
        _mapper = mapper;
    }

    public async Task<IEnumerable<TResponseContract>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        var entities = await _queryBuilder.GetAllAsync(filter);
        return MapToResponseContracts(entities);
    }

    public async Task<TResponseContract> GetByAsync(Expression<Func<TEntity, bool>> filter)
    {
        var entity = await _queryBuilder.GetByAsync(filter);
        return MapToResponseContract(entity);
    }

    public async Task<TResponseContract> GetByIdAsync(TId id)
    {
        var entity = await _queryBuilder.GetByIdAsync(id);
        return MapToResponseContract(entity);
    }

    public async Task<TResponseContract> AddAsync(TCreateRequestContract createRequest)
    {
        var entity = MapToEntity(createRequest);
        entity = await _queryBuilder.AddAsync(entity);
        return MapToResponseContract(entity);
    }

    public async Task AddBulkAsync(IEnumerable<TCreateRequestContract> createRequests)
    {
        var entities = createRequests.Select(MapToEntity);
        await _queryBuilder.AddBulkAsync(entities);
    }

    public async Task UpdateAsync(TUpdateRequestContract updateRequest)
    {
        var entity = MapToEntity(updateRequest);
        await _queryBuilder.UpdateAsync(entity);
    }

    public async Task UpdateChangedValuesOnlyAsync(TUpdateRequestContract updateRequest)
    {
        var entity = MapToEntity(updateRequest);
        await _queryBuilder.UpdateChangedValuesOnlyAsync(entity);
    }

    public async Task UpdateBulkAsync(IEnumerable<TUpdateRequestContract> updateRequests)
    {
        var entities = updateRequests.Select(MapToEntity);
        await _queryBuilder.UpdateBulkAsync(entities);
    }

    private TEntity MapToEntity(TCreateRequestContract createRequest)
    {
        return _mapper.Map<TEntity>(createRequest);
    }

    private TEntity MapToEntity(TUpdateRequestContract updateRequest)
    {
        return _mapper.Map<TEntity>(updateRequest);
    }

    private TResponseContract MapToResponseContract(TEntity entity)
    {
        return _mapper.Map<TResponseContract>(entity);
    }

    private IEnumerable<TResponseContract> MapToResponseContracts(IEnumerable<TEntity> entities)
    {
        return entities.Select(MapToResponseContract);
    }
}
