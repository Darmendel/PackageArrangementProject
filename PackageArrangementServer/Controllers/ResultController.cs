using Microsoft.AspNetCore.Mvc;
using PackageArrangementServer.Models;
using PackageArrangementServer.Services;

namespace PackageArrangementServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private IRabbitMqConsumerService consumerService;

        public ResultController(IRabbitMqConsumerService cs) { consumerService = cs; }

        [HttpPost]
        public string Post(string result)
        {
            Response.StatusCode = 200;
            Console.WriteLine(result);
            return result;
        }
    }
}
