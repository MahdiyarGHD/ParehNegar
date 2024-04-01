using EasyMicroservices.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using ParehNegar.Database.Database.Entities.Contents;
using ParehNegar.Domain.Contracts.Contents;
using ParehNegar.Logics.DatabaseLogics;
using ParehNegar.Logics.Interfaces;
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
        private readonly IContractLogic<long, ContentEntity, ContentContract, ContentContract, ContentContract> _contentLogic = unitOfWork.GetLongContractLogic<ContentEntity, ContentContract>();
        private readonly IContractLogic<long, ContentCategoryEntity, ContentCategoryContract, ContentCategoryContract, ContentCategoryContract> _categoryLogic = unitOfWork.GetLongContractLogic<ContentCategoryEntity, ContentCategoryContract>();
        private readonly IContractLogic<long, LanguageEntity, LanguageContract, LanguageContract, LanguageContract> _languageLogic = unitOfWork.GetLongContractLogic<LanguageEntity, LanguageContract>();

        public async Task<MessageContract<long>> Add(ContentContract request)
        {
            var t = unitOfWork.GetLongContractLogic<LanguageEntity, LanguageContract>();
            var language = await _languageLogic.GetByIdAsync(request.LanguageId).AsCheckedResult(x => x.Result);
            var category = await _categoryLogic.GetByIdAsync(request.CategoryId).AsCheckedResult(x => x.Result);
            return await _contentLogic.AddAsync(request);
        }

        public async Task<MessageContract<ContentContract>> Update(ContentContract request)
        {
            var language = await _languageLogic.GetByIdAsync(request.LanguageId).AsCheckedResult(x => x.Result);
            var category = await _categoryLogic.GetByIdAsync(request.CategoryId).AsCheckedResult(x => x.Result);
            return await _contentLogic.UpdateAsync(request);
        }

        public async Task<MessageContract<ContentContract>> GetByLanguage(GetByLanguageRequestContract request)
        {
            var getCategoryResult = await _categoryLogic.GetByAsync(c => c.Key.Equals(request.Key), 
                exp => exp.Include(q => q.Contents)
                .ThenInclude(q => q.Language));

            if (!getCategoryResult)
                return getCategoryResult.ToContract<ContentContract>();

            var contentResult = getCategoryResult.Result.Contents
                .FirstOrDefault(x => x.Language.Name.Equals(request.Language, StringComparison.OrdinalIgnoreCase));

            contentResult ??= getCategoryResult.Result.Contents.FirstOrDefault();
            
            if (contentResult is not { })
                return (FailedReasonType.NotFound, $"Content {request.Key} by language {request.Language} cannot be found!");

            return contentResult;
        }

        public async Task<ListMessageContract<ContentContract>> GetAllByKey(GetAllByKeyRequestContract request)
        {
            var getCategoryResult = await _categoryLogic
                .GetByAsync(c => c.Key.Equals(request.Key),
                    query => query
                    .Include(x => x.Contents)
                    .ThenInclude(x => x.Language));

            if (!getCategoryResult)
                return getCategoryResult.ToListContract<ContentContract>();

            return getCategoryResult.Result.Contents;
        }

        public async Task<MessageContract<ContentCategoryContract>> AddContentWithKey(AddContentWithKeyRequestContract request)
        {
            var getCategoryResult = await _categoryLogic.GetByAsync(x => x.Key == request.Key);
            if (getCategoryResult.IsSuccess)
                return (FailedReasonType.Duplicate, $"Category {request.Key} already exists.");

            var languages = await _languageLogic.GetAllAsync();
            var notFoundLanguages = request.LanguageData.Select(x => x.Language).Except(languages.Result.Select(o => o.Name));

            if (!notFoundLanguages.Any())
            {
                var addCategoryResult = await _categoryLogic.AddAsync(new ContentCategoryContract
                {
                    Key = request.Key
                });

                if (!addCategoryResult.IsSuccess)
                    return addCategoryResult.ToContract<ContentCategoryContract>();

                foreach (var item in request.LanguageData)
                {
                    var languageId = languages.Result.FirstOrDefault(o => o.Name == item.Language)?.Id;
                    if (!languageId.HasValue)
                        return (FailedReasonType.NotFound, $"Language {item.Language} not found!");

                    var addContentResult = await _contentLogic.AddAsync(new ContentContract
                    {
                        CategoryId = addCategoryResult.Result,
                        LanguageId = languageId.Value,
                        Data = item.Data
                    });
                }

                var addedCategoryResult = await _categoryLogic.GetByIdAsync(addCategoryResult.Result).AsCheckedResult(x => x.Result);
                return addedCategoryResult;
            }

            return (FailedReasonType.Incorrect, $"These languages are not registered in the language table: {string.Join(", ", notFoundLanguages)}");
        }

        public async Task<MessageContract> UpdateContentWithKey(AddContentWithKeyRequestContract request)
        {
            var getCategoryResult = await _categoryLogic.GetByAsync(
                x => x.Key == request.Key,
                query => query
                .Include(x => x.Contents)
                 .ThenInclude(x => x.Language)).AsCheckedResult(x => x.Result);

            List<ContentContract> contents = getCategoryResult.Contents;
            foreach (var content in contents)
            {
                if (request.LanguageData.Any(o => o.Language == content.Language.Name))
                {
                    var response = await _contentLogic.UpdateAsync(new ContentContract
                    {
                        Id = content.Id,
                        CategoryId = content.CategoryId,
                        LanguageId = content.LanguageId,
                        Data = request.LanguageData.FirstOrDefault(o => o.Language == content.Language.Name).Data
                    });

                    if (!response.IsSuccess)
                        return response.ToContract();
                }
            }

            foreach (var languageData in request.LanguageData)
            {
                if (!contents.Any(o => o.Language.Name == languageData.Language))
                {
                    var language = await _languageLogic.GetByAsync(o => o.Name == languageData.Language);

                    if (!language)
                        continue;

                    var response = await _contentLogic.AddAsync(new ContentContract
                    {
                        CategoryId = getCategoryResult.Id,
                        LanguageId = language.Result.Id,
                        Data = languageData.Data
                    });

                    if (!response)
                        return response.ToContract();
                }
            }

            return true;
        }
    }
}
