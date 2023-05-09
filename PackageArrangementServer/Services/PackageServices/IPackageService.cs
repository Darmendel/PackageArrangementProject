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
        //public Package ConvertToPackage(RequestCreationOfNewPackage request);

        /// <summary>
        /// Converts a request to create a package to a package.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="request"></param>
        /// <returns>Package</returns>
        public Package ConvertToPackage(string deliveryId, RequestCreationOfNewPackageInNewDelivery request);

        /// <summary>
        /// Converts a request to create a package to a package.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="request"></param>
        /// <returns>Package</returns>
        public Package ConvertToPackage(string deliveryId, RequestEditPackage request);

        public List<Package> GetPackageList(string deliveryId, List<RequestCreationOfNewPackageInNewDelivery> packages);

        /// <summary>
        /// Given a delivery id, adds a new package.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="amount"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        /// <param name="address"></param>
        /// <returns>Package</returns>
        public Package Create(string deliveryId, string amount = null, string width = null, string height = null,
            string depth = null, string address = null);

        /// <summary>
        /// Updates a package.
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="deliveryId"></param>
        /// <param name="amount"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        /// <param name="address"></param>
        /// <returns>Package</returns>
        public Package Edit(string packageId, string deliveryId, string amount = null, string width = null,
            string height = null, string depth = null, string address = null);

        /// <summary>
        /// Updates a package list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="package"></param>
        /// <returns>List<Package></returns>
        public List<Package> EditPackageList(List<Package> list, Package package);

        /// <summary>
        /// Deletes a package.
        /// </summary>
        /// <param name="packageId"></param>
        /// <param name="deliveryId"></param>
        /// <returns>Package</returns>
        public Package Delete(string packageId, string deliveryId);
    }
}
