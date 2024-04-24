using Azure.Messaging;
using EasyMicroservices.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ParehNegar.Database.Database.Entities;
using ParehNegar.Database.Database.Entities.Contents;
using ParehNegar.Domain.Attributes;
using ParehNegar.Domain.Contracts;
using ParehNegar.Domain.Contracts.Contents;
using ParehNegar.Logics.DatabaseLogics;
using ParehNegar.Logics.Interfaces;
using ParehNegar.Logics.Logics;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Dynamic;
using System.Net.Security;
using System.Reflection;

namespace ParehNegar.WebApi.Controllers;

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
    //    return await unitOfWork.GetLongContractLogic<PersonEntity, PersonContract>().GetAllAsync();
    //}


    [HttpGet]
    public async Task<ListMessageContract<ContentCategoryContract>> GetAllContents()
    {
            return await unitOfWork.GetLongContractLogic<ContentCategoryEntity, ContentCategoryContract>().GetAllAsync(expressions: q => q.Include(o => o.Contents));
        }

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
    //    var logic = unitOfWork.GetLongContractLogic<PersonEntity, PersonContract>();

    //    var test = await logic.AddAsync(new PersonContract { LastName = "Ghannad", Name = "Mahdiyar", Age = 12 });

    //    return test;
    //}

    //[HttpGet]
    //public async Task<MessageContract> TestAddBulk()
    //{
    //    var logic = unitOfWork.GetLongContractLogic<PersonEntity, PersonContract>();

    //    var test = await logic.AddBulkAsync([new PersonContract { LastName = "Doe", Name = "Mahi", Age = 15 }, new PersonContract { LastName = "Shakhmoradi", Name = "John", Age = 18 }]);

    //    return test;
    //}

    //[HttpGet]
    //public async Task<MessageContract> TestUpdate()
    //{
    //    var logic = unitOfWork.GetLongContractLogic<PersonEntity, PersonContract>();

    //    var test = await logic.UpdateAsync(new PersonContract { Id = 1, LastName = "xcxcxcx", Name = "afafaasf", Age = 12 });

    //    return test;
    //}

    //[HttpGet]
    //public async Task<MessageContract> TestUpdateBulk()
    //{
    //    var logic = unitOfWork.GetLongContractLogic<PersonEntity, PersonContract>();

    //    var test = await logic.UpdateBulkAsync([new PersonContract { Id = 1, LastName = "xcxcxcx", Name = "afafaasf", Age = 12 }, new PersonContract { Id = 2, LastName = "hhhhhhhhhh", Name = "ggggggg", Age = 15 }]);

    //    return test;
    //}


    //[HttpGet]
    //public async Task<MessageContract> UpdateChangedValuesAsync()
    //{
    //    var logic = unitOfWork.GetLongContractLogic<PersonEntity, PersonContract>();

    //    var test = await logic.UpdateChangedValuesOnlyAsync(new PersonContract { Id = 1, LastName = null, Name = "afafaasf", Age = 15 });

    //    return test;
    //}

    //[HttpGet]
    //public async Task<MessageContract<PersonContract>> GetPersonWithContent(long id)
    //{
    //    var logic = unitOfWork.GetLongContractLogic<PersonEntity, PersonContract>();
    //    PersonContract person = await logic.GetByIdAsync(id);
    //    return person;
    //}
}