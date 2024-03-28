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
    Task<ListMessageContract<TResponseContract>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null);
    Task<MessageContract<TResponseContract>> GetByAsync(Expression<Func<TEntity, bool>> filter);
    Task<MessageContract<TResponseContract>> GetByIdAsync(TId id);
    Task<MessageContract<TResponseContract>> AddAsync(TCreateRequestContract createRequest);
    Task<MessageContract> AddBulkAsync(IEnumerable<TCreateRequestContract> createRequests);
    Task<MessageContract<TResponseContract>> UpdateAsync(TUpdateRequestContract updateRequest);
    Task<MessageContract<TResponseContract>> UpdateChangedValuesOnlyAsync(TUpdateRequestContract updateRequest);
    Task<MessageContract> UpdateBulkAsync(IEnumerable<TUpdateRequestContract> updateRequests);
}
