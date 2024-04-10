using EasyMicroservices.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParehNegar.Database.Database.Entities.Contents;
using ParehNegar.Domain.Contracts.Contents;
using ParehNegar.Logics.DatabaseLogics;
using ParehNegar.Logics.Helpers;
using ParehNegar.Logics.Logics;

namespace ParehNegar.WebApi.Controllers.Contents
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContentController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpPost]
        public async Task<MessageContract> AddContentWithKey(AddContentWithKeyRequestContract request)
        {
            var categoryLogic = unitOfWork.GetLongContractLogic<ContentCategoryEntity, ContentCategoryContract>();
            var category = await categoryLogic.GetByAsync(x => x.Key.Equals(request.Key));
            if(!category.IsSuccess)
                return await unitOfWork.GetContentHelper().AddContentWithKey(request);

            var updateResponse = await unitOfWork.GetContentHelper().UpdateContentWithKey(request);
            return !updateResponse.IsSuccess ? updateResponse.ToContract() : updateResponse;
        }
        
        [HttpPost]
        public async Task<MessageContract> AddBulkContentWithKey(List<AddContentWithKeyRequestContract> request)
        {
            List<Task<MessageContract>> tasks = [];
            tasks.AddRange(request.Select(req => AddContentWithKey(req)));

            return (await Task.WhenAll(tasks)).All(x => x.IsSuccess);
        }

        [HttpPost]
        public async Task<MessageContract<ContentResponseContract>> GetByLanguage(GetByLanguageRequestContract request)
        {
            return await unitOfWork.GetContentHelper().GetByLanguage(request);
        }

        [HttpPost]
        public async Task<MessageContract> DeleteByKey(DeleteByKeyRequestContract request)
        {
            var categoryLogic = unitOfWork.GetLongContractLogic<ContentCategoryEntity, ContentCategoryContract>();
            ContentCategoryContract category = await categoryLogic.GetByAsync(x => x.Key.Equals(request.Key), q => q.Include(x => x.Contents)).AsCheckedResult(x => x.Result);

            return (await categoryLogic.HardDeleteByIdAsync(category.Id)).IsSuccess;
        }
    }
}
