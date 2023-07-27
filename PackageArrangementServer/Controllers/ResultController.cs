using Microsoft.AspNetCore.Mvc;
using PackageArrangementServer.Models;
using PackageArrangementServer.Models.Requests.RequestCreation;
using PackageArrangementServer.Services;
using PackageArrangementServer.Services.ResultServices;

namespace PackageArrangementServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private IResultService resultService;

        public ResultController(IResultService rs) { resultService = rs; }

        /// <summary>
        /// Handle delivery arrangement.
        /// </summary>
        /// <param name="req"></param>
        [HttpPost("DeliveryArrangement")]
        public string Post([FromBody] DeliveryTwoResults req)
        {
            try
            {
                string result = resultService.DeliveryArrangement(req);

                Response.StatusCode = result == null ? 200 : 400;
                Console.WriteLine(req);
                return result;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                Response.StatusCode = 400;
                return null;
            }
            
        }
    }
}
