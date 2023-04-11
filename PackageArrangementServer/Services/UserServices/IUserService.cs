using PackageArrangementServer.Models;
using PackageArrangementServer.Models.Containers;

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

        /*/// <summary>
        /// Creates a new user.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>int</returns>
        public int Add(string id, string name, string email, string password);*/

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>User</returns>
        public User Create(string id, string name, string email, string password);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="deliveries"></param>
        /// <returns>User</returns>
        public User Create(string id, string name, string email, string password, List<Delivery> deliveries = null);

        /// <summary>
        /// Updates a user.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="deliveries"></param>
        /// <returns>int</returns>
        public int Edit(string id, string name = null, string email = null, string password = null,
            List<Delivery> deliveries = null);

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

        /*/// <summary>
        /// Creates a new delivery for a user.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="packages"></param>
        /// <param name="container"></param>
        /// <returns>int</returns>
        public int AddDelivery(string userId, DateTime? deliveryDate = null, List<RequestCreationOfNewPackage>? packages = null,
            IContainer container = null);*/

        /// <summary>
        /// Creates a new delivery for a user.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="packages"></param>
        /// <param name="container"></param>
        /// <returns>int</returns>
        public int CreateDelivery(string userId, DateTime? deliveryDate = null, List<RequestCreationOfNewPackage>? packages = null,
            IContainer container = null);

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
        /// <param name="container"></param>
        /// <returns>int</returns>
        public int EditDelivery(string userId, string deliveryId, DateTime? deliveryDate = null,
            List<Package>? packages = null, IContainer container = null);

        /// <summary>
        /// Deletes a user's delivery.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>int</returns>
        public int DeleteDelivery(string userId, string deliveryId);

        /// <summary>
        /// Returns a list of all packages in a user's delivery.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>List<Package></returns>
        public List<Package> GetAllPackages(string userId, string deliveryId);

        /// <summary>
        /// Returns a user's package in a delivery.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="packageId"></param>
        /// <returns>Package</returns>
        public Package GetPackage(string userId, string deliveryId, string packageId);

        /// <summary>
        /// Adds a new package to a user's delivery.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        /// <param name="weight"></param>
        /// <param name="cost"></param>
        /// <param name="address"></param>
        /// <returns>int</returns>
        public int AddPackage(string userId, string deliveryId, string type = null, string amount = null, string width = null,
            string height = null, string depth = null, string weight = null, string cost = null, string address = null);

        /// <summary>
        /// Updates a package in a user's delivery.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="packageId"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        /// <param name="weight"></param>
        /// <param name="cost"></param>
        /// <param name="address"></param>
        /// <returns>int</returns>
        public int EditPackage(string userId, string deliveryId, string packageId, string type = null,
            string amount = null, string width = null, string height = null, string depth = null, string weight = null,
            string cost = null, string address = null);

        /// <summary>
        /// Deletes a package from a user's delivery.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public int DeletePackage(string userId, string deliveryId, string packageId);
    }
}
