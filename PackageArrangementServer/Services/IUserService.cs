﻿using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        /// <returns>List<User></returns>
        public List<User> GetAllUsers();

        /// <summary>
        /// Checks if a user exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public bool Exists(string id);

        /// <summary>
        /// Returns a user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        public User Get(string id);

        /// <summary>
        /// Creates a new user.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns>int</returns>
        public int Add(string id, string name, string password);

        /// <summary>
        /// Updates a user.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns>int</returns>
        public int Edit(string id, string? name = null, string? password = null);

        /// <summary>
        /// Deletes a user.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>int</returns>
        public int Delete(string id);

        /// <summary>
        /// Returns a list of a user's past (and current) deliveries.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<Delivery></returns>
        public List<Delivery> GetAllDeliveries(string id);

        /// <summary>
        /// Checks if a user have made a certain delivery before.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>bool</returns>
        //public bool DeliveryExists(string userId, string deliveryId);

        /// <summary>
        /// Returns a user's delivery by id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>Delivery</returns>
        public Delivery GetDelivery(string userId, string deliveryId);

        /// <summary>
        /// Creates a new delivery for a user.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="packages"></param>
        /// <param name="container"></param>
        /// <returns>int</returns>
        public int AddDelivery(string userId, DateTime? deliveryDate = null, List<Package>? packages = null, Container? container = null);

        /// <summary>
        /// Calculates a user's delivery cost.
        /// Returns -1 if there's no such delivery or user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>int</returns>
        public int GetDeliveryCost(string userId, string deliveryId);

        /// <summary>
        /// Evaluates a user's delivery status.
        /// Returns null if there's no such delivery or user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>DeliveryStatus</returns>
        public DeliveryStatus GetDeliveryStatus(string userId, string deliveryId);

        /// <summary>
        /// Updates a user's delivery.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="packages"></param>
        /// <param name="selectedContainer"></param>
        /// <returns>int</returns>
        public int EditDelivery(string userId, string deliveryId, DateTime? deliveryDate = null,
            List<Package>? packages = null, Container? selectedContainer = null);

        /// <summary>
        /// Deletes a user's delivery.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>int</returns>
        public int DeleteDelivery(string userId, string deliveryId);
    }
}
