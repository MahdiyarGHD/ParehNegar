using Azure.Messaging;
using EasyMicroservices.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using ParehNegar.Database.Database.Entities;
using ParehNegar.Database.Database.Entities.Contents;
using ParehNegar.Logics.DatabaseLogics;
using ParehNegar.Logics.Interfaces;
using ParehNegar.Logics.Logics;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Security;
using System.Reflection;

namespace ParehNegar.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController(IUnitOfWork unitOfWork) : ControllerBase
    {

        //[HttpGet]
        //public async Task<MessageContract> Add()
        //{
        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().AddAsync(new PersonContract { Age = 17, Name = "Mahdiyar" });
        //}

        //[HttpGet]
        //public async Task<MessageContract> GetById(long id)
        //{
        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().GetByIdAsync(id);
        //}

        //[HttpGet]
        //public async Task<MessageContract> Update()
        //{
        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().UpdateAsync(new PersonContract { Id = 1, Age = 2, Name = "Qzl" });
        //}

        //[HttpGet]
        //public async Task<ListMessageContract<PersonContract>> GetAll()
        //{
        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().GetAllAsync();
        //}

        //[HttpGet]
        //public async Task<MessageContract<PersonContract>> HardDeleteById(long id)
        //{
        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().HardDeleteByIdAsync(id);
        //}

        //[HttpGet]
        //public async Task<MessageContract<PersonContract>> SoftDeleteById(long id)
        //{
        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().SoftDeleteByIdAsync(id);
        //}

        //[HttpPost]
        //public async Task<MessageContract<int>> BulkSoftDeleteByIds(IEnumerable<long> ids)
        //{
        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().BulkSoftDeleteByIdAsync(ids);
        //}

        //[HttpPost]
        //public async Task<MessageContract<int>> BulkHardDeleteByIds(IEnumerable<long> ids)
        //{
        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().BulkHardDeleteByIdAsync(ids);

        //[HttpGet]
        //public async Task<MessageContract> Test()
        //{



        //    var logic = unitOfWork.GetLogic<ContentEntity, long>();
        //    var model = logic.Model;
        //    var entityType = model.FindEntityType(typeof(ContentEntity));
        //    var navigations = entityType.GetNavigations();
        //    return true;
        //}
    }
}