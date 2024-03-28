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
    Task<IEnumerable<TResponseContract>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null);
    Task<TResponseContract> GetByAsync(Expression<Func<TEntity, bool>> filter);
    Task<TResponseContract> GetByIdAsync(TId id);
    Task<TResponseContract> AddAsync(TCreateRequestContract createRequest);
    Task AddBulkAsync(IEnumerable<TCreateRequestContract> createRequests);
    Task<TResponseContract> UpdateAsync(TUpdateRequestContract updateRequest);
    Task<TResponseContract> UpdateChangedValuesOnlyAsync(TUpdateRequestContract updateRequest);
    Task UpdateBulkAsync(IEnumerable<TUpdateRequestContract> updateRequests);
}
