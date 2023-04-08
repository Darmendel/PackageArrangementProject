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
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns>int</returns>
        public int Cost(string deliveryId, string userId);

        /// <summary>
        /// Given a delivery id and a user id, evaluates the delivery's shipping status.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns>string</returns>
        public string Status(string deliveryId, string userId);

        /// <summary>
        /// Updates a delivery (changes it's list of packages or it's selected container and calculates it's new cost and status).
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="packages"></param>
        /// <param name="container"></param>
        public void Edit(string deliveryId, string userId, DateTime? deliveryDate = null, List<Package>? packages = null, Container? container = null); // maybe drop the userId + [int? cost = null, string? deliveryStatus = null]

        /// <summary>
        /// Deletes a delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        public void Delete(string deliveryId, string userId);

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
        public bool PackageExists(string deliveryId, string userId, string packageId); // maybe drop the userId

        /// <summary>
        /// Returns a package in a delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="packageId"></param>
        /// <returns>Package</returns>
        public Package GetPackage(string deliveryId, string userId, string packageId); // maybe drop the userId

        /// <summary>
        /// Returns number of packages in the delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns>int</returns>
        public int GetPackageCount(string deliveryId, string userId); // maybe drop the userId

        /// <summary>
        /// Adds a new package to the delivery. Returns 1 if succeeded and 0 otherwise.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="package"></param>
        /// <returns>int</returns>
        public int AddPackage(string deliveryId, Package package);

        /// <summary>
        /// Updates a package in a delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="packageId"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        /// <param name="cost"></param>
        /// <param name="adress"></param>
        public void EditPackage(string deliveryId, string userId, string packageId, string? type = null, string? amount = null, string? width = null, string? height = null, string? depth = null, string? cost = null, string? adress = null);

        /// <summary>
        /// Deletes a package from a delivery. Returns 1 if succeeded and 0 otherwise.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="packageId"></param>
        /// <returns>int</returns>
        public int DeletePackage(string deliveryId, string userId, string packageId); // maybe drop the userId
    }
}
