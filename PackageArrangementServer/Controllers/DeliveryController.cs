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
            // Global.deliveryId = Package.Claims.FirstOrDefault(claim => claim.Type == "deliveryId")?.Value;
            Response.StatusCode = 200;
            return deliveryService.GetAllPackages(Global.DeliveryId);
        }

        /// <summary>
        /// Returns the package with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Package</returns>
        [HttpGet("{id}")]
        public Package? Get(string id)
        {
            // Global.userId = Package.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;
            // Global.deliveryId = Package.Claims.FirstOrDefault(claim => claim.Type == "deliveryId")?.Value;
            Package package = deliveryService.GetPackage(id, Global.UserId, Global.DeliveryId);
            if (package == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Response.StatusCode = 200;
            return package;
        }
    }
}
