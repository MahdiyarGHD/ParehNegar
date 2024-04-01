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

public class ContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract>
        : IContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract>
    where TEntity : class, IIdSchema<TId> where TId : new()
{
    private readonly GenericQueryBuilder<TEntity, TId> _queryBuilder;
    private readonly IMapperProvider _mapper;
    private readonly DbContext _context;

    public ContractLogic(DbContext context, IMapperProvider mapper)
    {
        _context = context;
        _queryBuilder = new(context);
        _mapper = mapper;
    }

    public async Task<ListMessageContract<TResponseContract>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] expressions)
    {
        IEnumerable<TEntity> entities;

        entities = await _queryBuilder.GetAllAsync(filter, expressions);

        return MapToResponseContracts(entities);
    }

    public async Task<MessageContract<TResponseContract>> GetByAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>[] expressions)
    {
        var entity = await _queryBuilder.GetByAsync(filter, expressions);
        if (entity is null)
            return (FailedReasonType.NotFound, "Item by predicate not found!");
        return MapToResponseContract(entity);
    }

    public async Task<MessageContract<TResponseContract>> GetByIdAsync(TId id, params Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>[] expressions)
    {
        var entity = await _queryBuilder.GetByIdAsync(id, expressions);
        if (entity is null)
            return (FailedReasonType.NotFound, "Item by predicate not found!");
        return MapToResponseContract(entity);
    }

    public async Task<MessageContract<TId>> AddAsync(TCreateRequestContract createRequest)
    {
        var entity = MapToEntity(createRequest);
        entity = await _queryBuilder.AddAsync(entity);
        return entity.Id;
    }

    public async Task<MessageContract> AddBulkAsync(IEnumerable<TCreateRequestContract> createRequests)
    {
        var entities = createRequests.Select(MapToEntity);
        await _queryBuilder.AddBulkAsync(entities);
        return true;
    }

    public async Task<MessageContract<TResponseContract>> UpdateAsync(TUpdateRequestContract updateRequest)
    {
        var entity = MapToEntity(updateRequest);
        return MapToResponseContract(await _queryBuilder.UpdateAsync(entity));
    }

    public async Task<MessageContract<TResponseContract>> UpdateChangedValuesOnlyAsync(TUpdateRequestContract updateRequest)
    {
        var entity = MapToEntity(updateRequest);
        return MapToResponseContract(await _queryBuilder.UpdateChangedValuesOnlyAsync(entity));
    }

    public async Task<MessageContract> UpdateBulkAsync(IEnumerable<TUpdateRequestContract> updateRequests)
    {
        var entities = updateRequests.Select(MapToEntity);
        await _queryBuilder.UpdateBulkAsync(entities);
        return true;
    }

    public async Task<MessageContract<TResponseContract>> HardDeleteByIdAsync(TId id)
    {
        var entity = await _queryBuilder.HardDeleteByIdAsync(id);
        if (entity is null)
            return (FailedReasonType.NotFound, "Item by predicate not found!");
        return MapToResponseContract(entity);
    }

    public async Task<MessageContract<TResponseContract>> SoftDeleteByIdAsync(TId id)
    {
        var entity = await _queryBuilder.SoftDeleteByIdAsync(id);
        if (entity is null)
            return (FailedReasonType.NotFound, "Item by predicate not found!");
        return MapToResponseContract(entity);
    }

    public async Task<MessageContract<int>> BulkHardDeleteByIdAsync(IEnumerable<TId> ids)
    {
        return await _queryBuilder.BulkHardDeleteByIdAsync(ids);
    }

    public async Task<MessageContract<int>> BulkSoftDeleteByIdAsync(IEnumerable<TId> ids)
    {
        return await _queryBuilder.BulkSoftDeleteByIdAsync(ids);
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

    private List<TResponseContract> MapToResponseContracts(IEnumerable<TEntity> entities)
    {
        return entities.Select(MapToResponseContract).ToList();
    }
}
