using Microsoft.AspNetCore.Mvc;
using PackageArrangementServer.Models;
using PackageArrangementServer.Services;

namespace PackageArrangementServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private IDeliveryService deliveryService;

        public DeliveryController(IDeliveryService ds) { deliveryService = ds; }

        /// <summary>
        /// Returns all the packages of a delivery.
        /// </summary>
        /// <returns>List<Package></returns>
        [HttpGet]
        public List<Package> Get()
        {
            // Global.Id = Package.Claims.FirstOrDefault(claim => claim.Type == "PackageId")?.Value;
            Response.StatusCode = 200;
            return deliveryService.GetAllPackages(Global.Id);
        }
    }
}
