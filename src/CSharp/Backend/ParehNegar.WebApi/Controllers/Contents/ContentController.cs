﻿using EasyMicroservices.ServiceContracts;
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
            if (!updateResponse.IsSuccess)
                return updateResponse.ToContract();

            return updateResponse;
        }

        [HttpPost]
        public async Task<MessageContract<ContentResponseContract>> GetByLanguage(GetByLanguageRequestContract request)
        {
            return await unitOfWork.GetContentHelper().GetByLanguage(request);
        }

        [HttpDelete]
        public async Task<MessageContract> DeleteByKey(DeleteByKeyRequestContract request)
        {
            var categoryLogic = unitOfWork.GetLongContractLogic<ContentCategoryEntity, ContentCategoryContract>();
            ContentCategoryContract category = await categoryLogic.GetByAsync(x => x.Key.Equals(request.Key), q => q.Include(x => x.Contents)).AsCheckedResult(x => x.Result);

            var response = await categoryLogic.HardDeleteByIdAsync(category.Id);
            return response.IsSuccess;
        }
    }
}