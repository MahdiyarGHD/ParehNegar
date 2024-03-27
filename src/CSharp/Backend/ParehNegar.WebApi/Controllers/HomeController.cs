using Azure.Messaging;
using EasyMicroservices.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.Net.Security;

namespace ParehNegar.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
            خوشتیپ();
        }

        [HttpGet]
        public MessageContract<dynamic> خوشتیپ()
        {
            return new { Hello = "there" };
        }
    }
}
