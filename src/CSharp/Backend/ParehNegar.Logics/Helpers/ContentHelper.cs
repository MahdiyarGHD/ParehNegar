using EasyMicroservices.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using ParehNegar.Database.Database.Entities.Contents;
using ParehNegar.Domain.Contracts.Contents;
using ParehNegar.Logics.DatabaseLogics;
using ParehNegar.Logics.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Logics.Helpers
{
    public class ContentHelper(IUnitOfWork unitOfWork)
    {
        private readonly LongContractLogic<ContentEntity, ContentContract> _contentLogic = unitOfWork.GetLongContractLogic<ContentEntity, ContentContract>();
        private readonly LongContractLogic<ContentCategoryEntity, ContentCategoryContract> ـcategorylogic = unitOfWork.GetLongContractLogic<ContentCategoryEntity, ContentCategoryContract>();
        private readonly LongContractLogic<LanguageEntity, LanguageContract> ـlanguageLogic = unitOfWork.GetLongContractLogic<LanguageEntity, LanguageContract>();

        public async Task<MessageContract<long>> Add(ContentContract request)
        {
            var language = await ـlanguageLogic.GetByIdAsync(request.LanguageId).AsCheckedResult(x => x.Result);
            var category = await ـcategorylogic.GetByIdAsync(request.CategoryId).AsCheckedResult(x => x.Result);
            return await _contentLogic.AddAsync(request);
        }

        public async Task<MessageContract<ContentContract>> Update(ContentContract request)
        {
            var language = await ـlanguageLogic.GetByIdAsync(request.LanguageId).AsCheckedResult(x => x.Result);
            var category = await ـcategorylogic.GetByIdAsync(request.CategoryId).AsCheckedResult(x => x.Result);
            return await _contentLogic.UpdateAsync(request);
        }

        public async Task<MessageContract<ContentContract>> GetByLanguage(GetByLanguageRequestContract request)
        {
            var getCategoryResult = await ـcategorylogic.GetByAsync(c => c.Key.Equals(request.Key), 
                exp => exp.Include(q => q.Contents)
                .ThenInclude(q => q.Language));

            if (!getCategoryResult)
                return getCategoryResult.ToContract<ContentContract>();

            var contentResult = getCategoryResult.Result.Contents
                .FirstOrDefault(x => x.Language.Name.Equals(request.Language, StringComparison.OrdinalIgnoreCase));

            contentResult ??= getCategoryResult.Result.Contents.FirstOrDefault();
            
            if (contentResult is { })
                return (FailedReasonType.NotFound, $"Content {request.Key} by language {request.Language} cannot be found!");

            return contentResult;
        }

        public async Task<ListMessageContract<ContentContract>> GetAllByKey(GetAllByKeyRequestContract request)
        {
            var getCategoryResult = await ـcategorylogic
                .GetByAsync(c => c.Key.Equals(request.Key),
                    query => query
                    .Include(x => x.Contents)
                    .ThenInclude(x => x.Language));

            if (!getCategoryResult)
                return getCategoryResult.ToListContract<ContentContract>();

            return getCategoryResult.Result.Contents;
        }
    }
}
