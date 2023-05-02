using Microsoft.AspNetCore.Mvc;
using PackageArrangementServer.Models;
using PackageArrangementServer.Services;

namespace PackageArrangementServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        private RabbitMqClientBase rabbitMqClient;

        public UserController(IUserService us, RabbitMqClientBase rb)
        {
            userService = us;
            rabbitMqClient = rb;

            Global.UserId = "1";
            Global.DeliveryId = "1";
        }

        /// <summary>
        /// Checks if a user entered a valid id and a valid password.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        private bool Validate(string id, string password)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password)) return false;

            User user = userService.Get(id);
            if (user == null) return false;

            return user.Password == password;
        }

        /// <summary>
        /// Returns all users.
        /// </summary>
        /// <returns>List<User></returns>
        [HttpGet]
        public List<User> Get()
        {
            Response.StatusCode = 200;
            return userService.GetAllUsers();
        }

        /// <summary>
        /// Returns a user by id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User</returns>
        [HttpGet("userId")]
        public User? Get(string userId)
        {
            User user = userService.Get(userId);
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Response.StatusCode = 200;
            return user;
        }

        /// <summary>
        /// Returns a list of all deliveries.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List<Delivery></returns>
        [HttpGet("{userId}/deliveries")]
        public List<Delivery> GetDeliveries(string userId)
        {
            List<Delivery> deliveries = userService.GetAllDeliveries(userId);
            if (deliveries == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Response.StatusCode = 200;
            return deliveries;
        }

        /// <summary>
        /// Returns a user's delivery.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>Delivery</returns>
        [HttpGet("{userId}/deliveries/{deliveryId}")]
        public Delivery GetDelivery(string userId, string deliveryId)
        {
            Delivery delivery = userService.GetDelivery(userId, deliveryId);
            if (delivery == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Response.StatusCode = 200;
            return delivery;
        }

        /// <summary>
        /// Returns the cost of a user's delivery.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>string</returns>
        [HttpGet("{userId}/deliveries/{deliveryId}/cost")]
        public string GetDeliveryCost(string userId, string deliveryId)
        {
            int cost = userService.GetDeliveryCost(userId, deliveryId);
            if (cost == -1)
            {
                Response.StatusCode = 404;
                return null;
            }

            Response.StatusCode = 200;
            return cost.ToString();
        }

        /// <summary>
        /// Returns a user's delivery status.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>DeliveryStatus</returns>
        [HttpGet("{userId}/deliveries/{deliveryId}/status")]
        public DeliveryStatus? GetDeliveryStatus(string userId, string deliveryId)
        {
            DeliveryStatus status = userService.GetDeliveryStatus(userId, deliveryId);
            if (status == DeliveryStatus.NonExisting)
            {
                Response.StatusCode = 404;
                return null;
            }

            Response.StatusCode = 200;
            return status;
        }

        /// <summary>
        /// Returns a list of all packages in a user's delivery.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>List<Package></returns>
        [HttpGet("{userId}/deliveries/{deliveryId}/packages")]
        public List<Package> GetPackages(string userId, string deliveryId)
        {
            List<Package> packages = userService.GetAllPackages(userId, deliveryId);
            if (packages == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Response.StatusCode = 200;
            return packages;
        }

        /// <summary>
        /// Returns a user's package in a delivery.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="packageId"></param>
        /// <returns>Package</returns>
        [HttpGet("{userId}/deliveries/{deliveryId}/packages/{packageId}")]
        public Package GetPackage(string userId, string deliveryId, string packageId)
        {
            Package package = userService.GetPackage(userId, deliveryId, packageId);
            if (package == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Response.StatusCode = 200;
            return package;
        }

        /*/// <summary>
        /// Login.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>IActionResult</returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Post([FromBody] LoginRequest req)
        {
            throw new NotImplementedException(); // jwt?
        }*/

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="req"></param>
        [HttpPost("{id}/{name}/{password}")]
        //[ValidateAntiForgeryToken]
        public void Post([FromBody] RegisterRequest req)
        {
            if (userService.Create(req.Id, req.Name, req.Email, req.Password) == null) Response.StatusCode = 204;
            else Response.StatusCode = 400;
            return;
        }

        /// <summary>
        /// Creates a new delivery.
        /// </summary>
        /// <param name="req"></param>
        [HttpPost("{userId}/deliveries")]
        public void Post(string userId, [FromBody] RequestCreationOfNewDelivery req)
        {
            if (userService.CreateDelivery(userId, req.DeliveryDate, req.Packages) > 0)
                Response.StatusCode = 204;
            else Response.StatusCode = 400;
            return;
        }

        /// <summary>
        /// Adds a new package.
        /// </summary>
        /// <param name="req"></param>
        [HttpPost("{userId}/deliveries/{deliveryId}/packages")]
        public void Post(string userId, [FromBody] RequestCreationOfNewPackage req)
        {
            if (userService.CreatePackage(userId, req.DeliveryId, req.Amount, req.Width, req.Height,
                req.Depth, req.Address) > 0)
                Response.StatusCode = 204;
            else Response.StatusCode = 400;
            return;
        }

        /*/// <summary>
        /// Selects a container for a user's delivery.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="size"></param>
        [HttpPost("{userId}/deliveries/{deliveryId}/container")]
        public void Post(string userId, string deliveryId, ContainerSize size)
        {
        }*/

        /*[HttpPut("{userId}")]
        public async Task Put([FromBody] RegisterRequest req)
        {
        }*/

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="req"></param>
        [HttpPut("{userId}")]
        public void Put(string userId, [FromBody] RequestEditUser req)
        {
            if (userService.Edit(userId, req.Name, req.Email, req.Password, req.Deliveries) > 0)
                Response.StatusCode = 204;
            else Response.StatusCode = 400;
            return;
        }

        /*/// <summary>
        /// Updates a user's delivery.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="req"></param>
        [HttpPut("{userId}/deliveries/{deliveryId}")]
        public void Put(string userId, string deliveryId, [FromBody] RequestEditDelivery req)
        {
            if (userService.EditDelivery(userId, deliveryId, req.DeliveryDate, req.Packages,
                req.Container) > 0) Response.StatusCode = 204;
            else Response.StatusCode = 400;
            return;
        }*/

        [HttpPut("{userId}/deliveries/{deliveryId}/delivery-date")]
        public void PutDeliveryDate(string userId, string deliveryId, string deliveryDate)
        {
            
        }

        /// <summary>
        /// Updates a user's delivery's container.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="size"></param>
        [HttpPut("{userId}/deliveries/{deliveryId}/container")]
        public void PutCotainer(string userId, string deliveryId, ContainerSize size)
        {
            IContainer container = userService.GetContainer(size);
            if (container == null) Response.StatusCode = 400;
            else
            {
                if (userService.UpdateDelivery(userId, deliveryId, container) > 0) Response.StatusCode = 204;
                else Response.StatusCode = 400;
            }
            return;
        }

        /// <summary>
        /// Updates a user's delivery's container.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="depth"></param>
        [HttpPut("{userId}/deliveries/{deliveryId}/container/special-request")]
        public void PutContainer(string userId, string deliveryId, string height, string width, string depth)
        {
            IContainer container = userService.CreateContainer(height, width, depth);
            if (container == null) Response.StatusCode = 400;
            else
            {
                if (userService.UpdateDelivery(userId, deliveryId, container) > 0) Response.StatusCode = 204;
                else Response.StatusCode = 400;
            }
            return;
        }

        /// <summary>
        /// Updates a package in a user's delivery.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="packageId"></param>
        /// <param name="req"></param>
        [HttpPut("{userId}/deliveries/{deliveryId}/packages/{packageId}")]
        public void Put(string userId, string deliveryId, string packageId, [FromBody] RequestEditPackage req)
        {
            if (userService.EditPackage(userId, deliveryId, packageId, req.Amount, req.Width, req.Height,
                req.Depth, req.Address) > 0)
                Response.StatusCode = 204;
            else Response.StatusCode = 400;
            return;
        }

        /// <summary>
        /// Deletes a package in a user's delivery.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="packageId"></param>
        [HttpDelete("{userId}/deliveries/{deliveryId}/packages/{packageId}")]
        public void Delete(string userId, string deliveryId, string packageId)
        {
            if (userService.DeletePackage(userId, deliveryId, packageId) > 0) Response.StatusCode = 204;
            else Response.StatusCode = 400;
            return;
        }

    }
}
