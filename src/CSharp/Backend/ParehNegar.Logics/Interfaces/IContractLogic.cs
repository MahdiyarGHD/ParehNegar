using EasyMicroservices.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Logics.Interfaces;

public interface IContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract>
    where TEntity : class
{
    Task<ListMessageContract<TResponseContract>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] expressions);
    Task<MessageContract<TResponseContract>> GetByAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>[] expressions);
    Task<MessageContract<TResponseContract>> GetByIdAsync(TId id, params Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>>[] expressions);
    Task<MessageContract<TId>> AddAsync(TCreateRequestContract createRequest);
    Task<MessageContract> AddBulkAsync(IEnumerable<TCreateRequestContract> createRequests);
    Task<MessageContract<TResponseContract>> UpdateAsync(TUpdateRequestContract updateRequest);
    Task<MessageContract<TResponseContract>> UpdateChangedValuesOnlyAsync(TUpdateRequestContract updateRequest);
    public Task<MessageContract<TResponseContract>> HardDeleteByIdAsync(TId id);
    public Task<MessageContract<TResponseContract>> SoftDeleteByIdAsync(TId id);
    public Task<MessageContract<int>> BulkHardDeleteByIdAsync(IEnumerable<TId> ids);
    public Task<MessageContract<int>> BulkSoftDeleteByIdAsync(IEnumerable<TId> ids);
    Task<MessageContract> UpdateBulkAsync(IEnumerable<TUpdateRequestContract> updateRequests);
}
