using Azure.Messaging;
using EasyMicroservices.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
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

        
    }
}
