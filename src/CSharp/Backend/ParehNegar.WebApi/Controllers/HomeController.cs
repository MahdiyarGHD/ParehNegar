//using Azure.Messaging;
//using EasyMicroservices.ServiceContracts;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Diagnostics;
//using ParehNegar.Database.Database.Entities;
//using ParehNegar.Database.Database.Entities.Contents;
//using ParehNegar.Domain.Attributes;
//using ParehNegar.Domain.Contracts;
//using ParehNegar.Domain.Contracts.Contents;
//using ParehNegar.Logics.DatabaseLogics;
//using ParehNegar.Logics.Interfaces;
//using ParehNegar.Logics.Logics;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data;
//using System.Dynamic;
//using System.Net.Security;
//using System.Reflection;

//namespace ParehNegar.WebApi.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class HomeController(IUnitOfWork unitOfWork) : ControllerBase
//    {

//        //[HttpGet]
//        //public async Task<MessageContract> Add()
//        //{
//        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().AddAsync(new PersonContract { Age = 17, Name = "Mahdiyar" });
//        //}

//        //[HttpGet]
//        //public async Task<MessageContract> GetById(long id)
//        //{
//        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().GetByIdAsync(id);
//        //}

//        //[HttpGet]
//        //public async Task<MessageContract> Update()
//        //{
//        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().UpdateAsync(new PersonContract { Id = 1, Age = 2, Name = "Qzl" });
//        //}

//        [HttpGet]
//        public async Task<ListMessageContract<PersonContract>> GetAll()
//        {
//            return await unitOfWork.GetLongContractLogic<PersonEntity, PersonContract>().GetAllAsync();
//        }


//        [HttpGet]
//        public async Task<ListMessageContract<ContentResponseContract>> GetAllContents()
//        {
//            return await unitOfWork.GetLongContractLogic<ContentEntity, ContentResponseContract>().GetAllAsync(expressions: q => q.Include(o => o.Category));
//        }

//        //[HttpGet]
//        //public async Task<MessageContract<PersonContract>> HardDeleteById(long id)
//        //{
//        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().HardDeleteByIdAsync(id);
//        //}

//        //[HttpGet]
//        //public async Task<MessageContract<PersonContract>> SoftDeleteById(long id)
//        //{
//        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().SoftDeleteByIdAsync(id);
//        //}

//        //[HttpPost]
//        //public async Task<MessageContract<int>> BulkSoftDeleteByIds(IEnumerable<long> ids)
//        //{
//        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().BulkSoftDeleteByIdAsync(ids);
//        //}

//        //[HttpPost]
//        //public async Task<MessageContract<int>> BulkHardDeleteByIds(IEnumerable<long> ids)
//        //{
//        //    return await unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>().BulkHardDeleteByIdAsync(ids);

//        [HttpGet]
//        public async Task<MessageContract> Test()
//        {
//            var logic = unitOfWork.GetLongContractLogic<PersonEntity, PersonContract>();

//            var test = await logic.AddAsync(new PersonContract { LastName = "Ghannad", Name = "Mahdiyar", Age = 12 });

//            return test;
//            //var contentHelper = unitOfWork.GetContentLanguageHelper();


//            //PersonContract req = new() { Id = test.Result, Name = "Mahdiyar", LastName = "Ghannad" };

//            //Type modifiedContractType = ContentTypeModifier.ModifyMultilingualRequestType(typeof(PersonContract));
//            //var newRequest = Activator.CreateInstance(modifiedContractType);

//            //if (!Attribute.IsDefined(modifiedContractType, typeof(ContentIdentifierAttribute)))
//            //    return false;

//            //foreach (var prop in modifiedContractType.GetRuntimeProperties())
//            //{
//            //    if (Attribute.IsDefined(prop, typeof(ContentLanguageAttribute)))
//            //        prop.SetValue(newRequest, new List<LanguageDataContract> {
//            //        new ()
//            //        {
//            //            Language = "fa-IR",
//            //            Data = req?.GetType()?.GetProperty(prop.Name)?.GetValue(req) as string ?? ""
//            //        }});
//            //    else
//            //        prop.SetValue(newRequest, req?.GetType()?.GetProperty(prop.Name)?.GetValue(req));
//            //}

//            //await contentHelper.AddToContentLanguage(newRequest);

//            //return test;
//        }

//        [HttpGet]
//        public async Task<MessageContract<PersonContract>> GetPersonWithContent(long id)
//        {
//            var logic = unitOfWork.GetLongContractLogic<PersonEntity, PersonContract>();
//            PersonContract person = await logic.GetByIdAsync(id);
//            return person;
//        }
//    }
//}