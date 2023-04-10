﻿using Microsoft.AspNetCore.Mvc;
using PackageArrangementServer.Models;
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

        /// <summary>
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
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="req"></param>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("{id}/{name}/{password}")]
        //[ValidateAntiForgeryToken]
        public void Post([FromBody] RegisterRequest req)
        {
            if (userService.Add(req.Id, req.Name, req.Email, req.Password) > 0) Response.StatusCode = 204;
            else Response.StatusCode = 400;
            return;
        }

    }
}
