using Microsoft.AspNetCore.Mvc;
using PackageArrangementServer.Models;
using PackageArrangementServer.Models.Requests.RequestCreation;
using PackageArrangementServer.Services;

namespace PackageArrangementServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService us)
        {
            userService = us;
        }

        /// <summary>
        /// Sign In.
        /// </summary>
        /// <param name="req"></param>
        [HttpPost("Login")]
        //[ValidateAntiForgeryToken] 
        public string Post([FromBody] LoginRequest req)
        {
            try
            {
                string id = userService.Login(req);
                Response.StatusCode = id != null ? 200 : 401;
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Response.StatusCode = 400;
                return null;
            }
            
        }


        /// <summary>
        /// Sign Up.
        /// </summary>
        /// <param name="req"></param>
        [HttpPost("SignUp")]
        //[ValidateAntiForgeryToken]
        public void Post([FromBody] RegisterRequest req)
        {
            try
            {
                if (userService.SignUpUser(req) == true) Response.StatusCode = 201;
                else Response.StatusCode = 400;
                return;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                Response.StatusCode = 400;
                return;
            }
            
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
            User user = userService.Get(userId, "id");
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Response.StatusCode = 200;
            return user;
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

        /// <summary>
        /// Adds a new package.
        /// </summary>
        /// <param name="req"></param>
        [HttpPost("{userId}/deliveries/{deliveryId}/packages")]
        public void Post(string userId, [FromBody] RequestCreationOfNewPackage req)
        {
            if (userService.CreatePackage(userId, req.DeliveryId, req.Width, req.Height,
                req.Length) > 0)
                Response.StatusCode = 204;
            else Response.StatusCode = 400;
            return;
        }

        /*/// <summary>
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
        }*/

        /*/// <summary>
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
        }*/

        /// <summary>
        /// Returns a list of all deliveries.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List<Delivery></returns>
        [HttpGet("{userId}/deliveries")]
        public List<Delivery> GetDeliveries(string userId)
        {
            try
            {
                List<Delivery> deliveries = userService.GetAllDeliveries(userId);
                if (deliveries == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                Response.StatusCode = 200;
                return deliveries;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = 400;
                return null;
            }
            
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
            try
            {
                Delivery delivery = userService.GetDelivery(userId, deliveryId);
                if (delivery == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                Response.StatusCode = 200;
                return delivery;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = 400;
                return null;
            }
            
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
            try
            {
                DeliveryStatus status = userService.GetDeliveryStatus(userId, deliveryId);
                if (status == DeliveryStatus.NonExisting)
                {
                    Response.StatusCode = 404;
                    return null;
                }

                Response.StatusCode = 200;
                return status;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = 400;
                return null;
            }
            
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
        /// Creates a new delivery.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>string</returns>
        [HttpPost("{userId}/deliveries")]
        public string Post(string userId, [FromBody] RequestCreationOfNewDelivery req)
        {
            try
            {
                string id;
                id = userService.CreateDelivery(userId, req.DeliveryDate, req.Packages, req.containerSize);
                Response.StatusCode = id != null ? 200 : 404;
                return id;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                Response.StatusCode = 400;
                return null;
            }
            
        }

        /// <summary>
        /// Creates a new delivery.
        /// </summary>
        /// <param name="req"></param>
        /// <returns>string</returns>
        [HttpPost("{userId}/deliveries/custompackage")]
        public string Post(string userId, [FromBody] RequestCreationDeliveryCustomContainer req)
        {
            try
            {
                string id;
                RequestCreationOfNewDeliveryCustomContainer newReq = new RequestCreationOfNewDeliveryCustomContainer(req);
                id = userService.CreateDelivery(userId, newReq);
                Response.StatusCode = id != null ? 200 : 404;
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Response.StatusCode = 400;
                return null;
            }
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
            if (userService.EditPackage(userId, deliveryId, packageId, req.Width, req.Height,
                req.Length) > 0)
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
