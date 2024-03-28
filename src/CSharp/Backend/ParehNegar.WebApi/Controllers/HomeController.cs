using Azure.Messaging;
using EasyMicroservices.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using ParehNegar.Database.Entities;
using ParehNegar.Domain.Contracts;
using ParehNegar.Logics.DatabaseLogics;
using ParehNegar.Logics.Interfaces;
using ParehNegar.Logics.Logics;
using System.Net.Security;

namespace ParehNegar.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<MessageContract> Add()
        {
            var logic = _unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>();
            await logic.AddAsync(new PersonContract { Name = "Mahdiyar", LastName = "Ghannad" });
            return true;
        }

        [HttpGet]
        public async Task<ListMessageContract<PersonContract>> GetAll()
        {
            var logic = _unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>();
            var result = await logic.GetAllAsync();
            return result.ToList();
        }

        [HttpGet]
        public async Task<MessageContract<PersonContract>> Update()
        {
            var logic = _unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>();


            var result = await logic.UpdateAsync(new PersonContract { Id = 1, LastName = "asfasfasfasfasf" });
            return result;
        }

        [HttpGet]
        public async Task<MessageContract<PersonContract>> UpdateChangedOnly()
        {
            var logic = _unitOfWork.GetContractLogic<long, PersonEntity, PersonContract, PersonContract, PersonContract>();
            var result = await logic.UpdateChangedValuesOnlyAsync(new PersonContract { Id = 1, Name = "Hello there" });
            return result;
        }
    }
}
