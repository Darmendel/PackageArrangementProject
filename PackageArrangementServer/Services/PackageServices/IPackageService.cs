using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IPackageService
    {
        /// <summary>
        /// Given a delivery's id, returns a list of packages.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns>List<Package></returns>
        public List<Package> GetAllPackages(string deliveryId);

        /// <summary>
        /// Checks if a package exists.
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>bool</returns>
        public bool Exists(string packageId, string deliveryId);

        /// <summary>
        /// Returns the number of packages in a delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns>int</returns>
        public int Count(string deliveryId);

        /// <summary>
        /// Returns a package by package id and delivery id.
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>Package</returns>
        public Package Get(string packageId, string deliveryId);

        /// <summary>
        /// Converts a request to create a package to a package.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Package</returns>
        public Package ConvertToPackage(RequestCreationOfNewPackage request);

        /// <summary>
        /// Given a delivery id, adds a new package.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
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
        public int Add(string deliveryId, string type = null, string amount = null, string width = null, string height = null,
            string depth = null, string weight = null, string cost = null, string address = null);

        /// <summary>
        /// Updates a package.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="packageId"></param>
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
        public int Edit(string packageId, string deliveryId, string type = null, string amount = null, string width = null,
            string height = null, string depth = null, string weight = null, string cost = null, string address = null);

        /// <summary>
        /// Deletes a package.
        /// Returns 1 if succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>int</returns>
        public int Delete(string packageId, string deliveryId);
    }
}
