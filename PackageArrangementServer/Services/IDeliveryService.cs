using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IDeliveryService
    {
        /// <summary>
        /// Given a delivey's is, returns a list of packages of the delivery.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<Package></returns>
        public List<Package> GetAllPackages(string id);

        /// <summary>
        /// Checks if a certain delivery exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public bool Exists(string id);

        /// <summary>
        /// Checks if a certain delivery exists.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns>bool</returns>
        public bool Exists(string id, string userId); // maybe drop the userId

        /// <summary>
        /// Checks if a certain package exists in a delivery.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="packageId"></param>
        /// <returns>bool</returns>
        public bool PackageExists(string id, string userId, string packageId); // maybe drop the userId

        //public int CreateDelivery();

        /// <summary>
        /// Updated a delivery (changes it's list of packages or it's selected container and calculates it's new cost and status).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="packages"></param>
        /// <param name="container"></param>
        public void Edit(string id, string userId, List<Package>? packages = null, Container? container = null); // maybe drop the userId + [int? cost = null, string? deliveryStatus = null]

        /// <summary>
        /// Updates a package in a delivery.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="packageId"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        /// <param name="isFragile"></param>
        /// <param name="cost"></param>
        /// <param name="adress"></param>
        public void EditPackage(string id, string userId, string packageId, string? type = null, int? amount = null, int? width = null, int? height = null, int? depth = null, bool? isFragile = null, int? cost = null, string? adress = null); // maybe drop the userId

        /// <summary>
        /// Returns a package in a delivery.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="packageId"></param>
        /// <returns>Package</returns>
        public Package GetPackage(string id, string userId, string packageId); // maybe drop the userId

        /// <summary>
        /// Returns number of packages in the delivery.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>int</returns>
        public int GetPackageCount(string id);

        /// <summary>
        /// Returns number of packages in the delivery.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns>int</returns>
        public int GetPackageCount(string id, string userId); // maybe drop the userId

        /// <summary>
        /// Adds a new package to the delivery. Returns 1 if succeeded and 0 otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="package"></param>
        /// <returns>int</returns>
        public int AddPackage(string id, Package package);

        /// <summary>
        /// Deletes a package from a delivery. Returns 1 if succeeded and 0 otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="packageId"></param>
        /// <returns>int</returns>
        public int DeletePackage(string id, string userId, string packageId); // maybe drop the userId
    }
}
