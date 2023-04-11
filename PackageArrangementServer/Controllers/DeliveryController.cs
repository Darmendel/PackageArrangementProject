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

        public DeliveryController(IDeliveryService ds)
        {
            deliveryService = ds;
            Global.UserId = "1";
            Global.DeliveryId = "1";
        }

        /// <summary>
        /// Returns all the packages of a delivery.
        /// </summary>
        /// <returns>List<Package></returns>
        [HttpGet]
        public List<Package> Get()
        {
            // Global.deliveryId = Package.Claims.FirstOrDefault(claim => claim.Type == "deliveryId")?.Value;
            Response.StatusCode = 200;
            return deliveryService.GetAllPackages(Global.DeliveryId, Global.UserId);
        }

        /// <summary>
        /// Returns a package by id.
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns>Package</returns>
        [HttpGet("{packageId}")]
        public Package? Get(string packageId)
        {
            // Global.userId = Package.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;
            // Global.deliveryId = Package.Claims.FirstOrDefault(claim => claim.Type == "deliveryId")?.Value;
            Package package = deliveryService.GetPackage(Global.DeliveryId, Global.UserId, packageId);
            if (package == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Response.StatusCode = 200;
            return package;
        }

        /// <summary>
        /// Returns all packages.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns>List<Package></returns>
        [HttpGet("{deliveryId}/packages")]
        public List<Package> GetPackages(string deliveryId)
        {
            List<Package> packageList = deliveryService.GetAllPackages(deliveryId, Global.UserId);
            if (packageList == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Response.StatusCode = 200;
            return packageList;
        }

        /// <summary>
        /// Creates a new delivery.
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public void Post([FromBody] RequestCreationOfNewDelivery request)
        {
            // Global.userId = Package.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;
            /*int res = deliveryService.Add(Global.UserId, request.DeliveryDate, request.Packages, request.Container);
            if (res > 0)
            {
                Response.StatusCode = 404;
                return;
            }
            Response.StatusCode = 201; // user needs update as well!*/
            throw new NotImplementedException();
        }
    }
}
