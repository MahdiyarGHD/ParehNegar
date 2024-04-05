using AutoMapper.Internal;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ParehNegar.Database.Database.Entities.Contents;
using ParehNegar.Domain;
using ParehNegar.Domain.Attributes;
using ParehNegar.Domain.BaseModels;
using ParehNegar.Domain.Contracts;
using ParehNegar.Domain.Contracts.Contents;
using ParehNegar.Logics.DatabaseLogics;
using ParehNegar.Logics.Helpers;
using ParehNegar.Logics.Interfaces;
using ParehNegar.Logics.Logics;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace ParehNegar.Logics.DatabaseLogics;

public class ContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract>(IBaseUnitOfWork unitOfWork, bool isMultilingual = false)
        : IContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract>
    where TEntity : class, IIdSchema<TId> where TId : new()
{
    private readonly GenericQueryBuilder<TEntity, TId> _queryBuilder = new(unitOfWork.GetDbContext());
    private readonly IMapperProvider _mapper = unitOfWork.GetMapper();
    private readonly string _defaultLanguage = unitOfWork.GetValue("DefaultSettings.Language");

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
        TCreateRequestContract requestToMap = EnsureContentPropertiesAreNull(createRequest);

        var entity = MapToEntity(requestToMap);
        entity = await _queryBuilder.AddAsync(entity);
        if (entity is not null)
        {
            createRequest?.GetType()?.GetProperty("Id", BindingFlags.Instance | BindingFlags.Public)?.SetValue(createRequest, entity.Id);
            await AddContentsIfNecessary(createRequest);
        }
        return entity.Id;
    }

    public async Task<MessageContract> AddBulkAsync(IEnumerable<TCreateRequestContract> createRequests)
    {
        var tasks = new List<Task>();

        var entities = createRequests.Select(request =>
        {
            TCreateRequestContract requestToMap = EnsureContentPropertiesAreNull(request);
            return MapToEntity(requestToMap);
        }).ToList();

        await _queryBuilder.AddBulkAsync(entities);

        foreach ((TCreateRequestContract createRequest, TEntity entity) in createRequests.Zip(entities, (c, e) => (c, e)))
        {
            createRequest?.GetType()?.GetProperty("Id", BindingFlags.Instance | BindingFlags.Public)?.SetValue(createRequest, entity.Id);
            tasks.Add(AddContentsIfNecessary(createRequest));
        }

        await Task.WhenAll(tasks);

        return true;
    }

    public async Task<MessageContract<TResponseContract>> UpdateAsync(TUpdateRequestContract updateRequest)
    {
        TUpdateRequestContract requestToMap = EnsureContentPropertiesAreNull(updateRequest);

        var entity = MapToEntity(requestToMap);
        if (entity is not null)
            await UpdateContentsIfNecessary(updateRequest);
        return MapToResponseContract(await _queryBuilder.UpdateAsync(entity));
    }

    public async Task<MessageContract<TResponseContract>> UpdateChangedValuesOnlyAsync(TUpdateRequestContract updateRequest)
    {
        TUpdateRequestContract requestToMap = EnsureContentPropertiesAreNull(updateRequest);

        var entity = MapToEntity(requestToMap);
        if (entity is not null)
            await UpdateContentsIfNecessary(updateRequest);
        
        return MapToResponseContract(await _queryBuilder.UpdateChangedValuesOnlyAsync(entity));
    }

    public async Task<MessageContract> UpdateBulkAsync(IEnumerable<TUpdateRequestContract> updateRequests)
    {
        var tasks = new List<Task>();

        var entities = updateRequests.Select(MapToEntity).ToList();

        await _queryBuilder.UpdateBulkAsync(entities);

        foreach (var updateRequest in updateRequests)
        {
            tasks.Add(UpdateContentsIfNecessary(updateRequest));
        }

        await Task.WhenAll(tasks);

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

    private object ModifyRequestContentIfNecessary<TContract>(TContract contract)
    {
        Type modifiedContractType = ContentTypeModifier.ModifyMultilingualRequestType(typeof(TCreateRequestContract));
        var newRequest = Activator.CreateInstance(modifiedContractType);

        if (Attribute.IsDefined(modifiedContractType, typeof(ContentIdentifierAttribute)))
            foreach (var prop in modifiedContractType.GetRuntimeProperties())
            {
                PropertyInfo? contractProp = contract?.GetType()?.GetProperty(prop.Name);
                object? contractPropValue = contractProp?.GetValue(contract);

                if (!Attribute.IsDefined(prop, typeof(ContentLanguageAttribute)))
                {
                    prop.SetValue(newRequest, contractPropValue);
                    continue;
                }

                if (contractProp.PropertyType.IsAssignableFrom(typeof(List<LanguageDataContract>)))
                    prop.SetValue(newRequest, contractPropValue);
                else
                    prop.SetValue(newRequest, new List<LanguageDataContract> {
                            new()
                            {
                                Language = _defaultLanguage ?? "en-US",
                                Data = contractPropValue as string ?? ""
                            }}
                    );
            }

        return newRequest;
    }

    private TContract EnsureContentPropertiesAreNull<TContract>(TContract contract)
    {
        TContract requestToMap = unitOfWork.GetMapper().Map<TContract>(contract);
        foreach (var property in requestToMap.GetType().GetProperties())
            if (Attribute.IsDefined(property, typeof(ContentLanguageAttribute)))
                property.SetValue(requestToMap, null);

        return requestToMap;
    }

    private async Task AddContentsIfNecessary(TCreateRequestContract contract)
    {
        List<PropertyInfo> contentLanguageProps = typeof(TCreateRequestContract).GetProperties()
            .Where(prop => Attribute.IsDefined(prop, typeof(ContentLanguageAttribute)))
            .ToList();

        if (contentLanguageProps.Count == 0)
            return;

        if (contentLanguageProps.Any(x => !Equals(x.PropertyType, typeof(IEnumerable<LanguageDataContract>))))
        {
            var newRequest = ModifyRequestContentIfNecessary(contract);

            await unitOfWork.GetContentLanguageHelper().AddToContentLanguage(newRequest);
        }
        else
            await unitOfWork.GetContentLanguageHelper().AddToContentLanguage(contract);
    }

    private async Task UpdateContentsIfNecessary(TUpdateRequestContract contract)
    {
        List<PropertyInfo> contentLanguageProps = typeof(TUpdateRequestContract).GetProperties()
            .Where(prop => Attribute.IsDefined(prop, typeof(ContentLanguageAttribute)))
            .ToList();

        if (contentLanguageProps.Count == 0)
            return;

        if (contentLanguageProps.Any(x => !Equals(x.PropertyType, typeof(IEnumerable<LanguageDataContract>))))
        {
            var newRequest = ModifyRequestContentIfNecessary(contract);

            await unitOfWork.GetContentLanguageHelper().UpdateToContentLanguage(newRequest);
        }
        else
            await unitOfWork.GetContentLanguageHelper().UpdateToContentLanguage(contract);
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
        TResponseContract mappedContract = _mapper.Map<TResponseContract>(entity);
        unitOfWork.GetContentLanguageHelper().ResolveContentLanguage(mappedContract, _defaultLanguage);
        return mappedContract;
    }

    private List<TResponseContract> MapToResponseContracts(IEnumerable<TEntity> entities)
    {
        return entities.Select(MapToResponseContract).ToList();
    }
}
