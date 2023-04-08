using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IDeliveryService
    {
        /// <summary>
        /// Given a user's id, returns a list of deliveries.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<Delivery></returns>
        public List<Delivery> GetAllDeliveries(string userId);

        /// <summary>
        /// Checks if a certain delivery exists.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns>bool</returns>
        public bool Exists(string deliveryId, string userId);

        /// <summary>
        /// Returns a delivery by delivery id and user id.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns>Delivery</returns>
        public Delivery Get(string deliveryId, string userId);

        //public int CreateDelivery();

        /// <summary>
        /// Given a delivery id and a user id, calculates the cost of a delivery.
        /// Returns -1 if there's no such delivery or user.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns>int</returns>
        public int Cost(string deliveryId, string userId);

        /// <summary>
        /// Given a delivery id and a user id, evaluates the delivery's shipping status.
        /// Returns null if there's no such delivery or user.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns>DeliveryStatus</returns>
        public DeliveryStatus Status(string deliveryId, string userId);

        /// <summary>
        /// Given a user id, adds a new delivery.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="packages"></param>
        /// <param name="container"></param>
        /// <param name="cost"></param>
        /// <returns>int</returns>
        public int Add(string userId, DateTime? deliveryDate = null, List<Package> packages = null,
            Container container = null, string cost = null);

        /// <summary>
        /// Updates a delivery (changes it's list of packages or it's selected container and calculates it's new cost and status).
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="packages"></param>
        /// <param name="container"></param>
        /// <returns>int</returns>
        public int Edit(string deliveryId, string userId, DateTime? deliveryDate = null, List<Package>? packages = null, Container? container = null); // [int? cost = null, string? deliveryStatus = null]

        /// <summary>
        /// Deletes a delivery.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns>int</returns>
        public int Delete(string deliveryId, string userId);

        /// <summary>
        /// Given a delivery id and a user id, returns all of the packages of the delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Package> GetAllPackages(string deliveryId, string userId);

        /// <summary>
        /// Checks if a certain package exists in a delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="packageId"></param>
        /// <returns>bool</returns>
        public bool PackageExists(string deliveryId, string userId, string packageId);

        /// <summary>
        /// Returns a package in a delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="packageId"></param>
        /// <returns>Package</returns>
        public Package GetPackage(string deliveryId, string userId, string packageId);

        /// <summary>
        /// Returns number of packages in the delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns>int</returns>
        public int GetPackageCount(string deliveryId, string userId);

        /// <summary>
        /// Adds a new package to the delivery.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        /// <param name="weight"></param>
        /// <param name="cost"></param>
        /// <param name="address"></param>
        /// <returns>int</returns>
        public int AddPackage(string deliveryId, string userId, string type = null, string amount = null,string width = null,
            string height = null, string depth = null, string weight = null, string cost = null, string address = null);

        /// <summary>
        /// Updates a package in a delivery.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
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
        public int EditPackage(string deliveryId, string userId, string packageId, string type = null,
            string amount = null, string width = null, string height = null, string depth = null, string weight = null,
            string cost = null, string address = null);

        /// <summary>
        /// Deletes a package from a delivery.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="packageId"></param>
        /// <returns>int</returns>
        public int DeletePackage(string deliveryId, string userId, string packageId);
    }
}
