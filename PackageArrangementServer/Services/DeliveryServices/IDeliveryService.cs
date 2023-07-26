using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IDeliveryService
    {
        /// <summary>
        /// Given a user's id, returns a list of deliveries.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List<Delivery></returns>
        public List<Delivery> GetAllDeliveries(string userId) { return null; }

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

        /// <summary>
        /// Given a delivery id and a user id, calculates the cost of a delivery.
        /// Returns -1 if there's no such delivery or user.
        /// </summary>
        /// <param name="delivery"></param>
        /// <returns>int</returns>
        public int Cost(Delivery delivery);

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
        /// <param name="delivery"></param>
        /// <returns>DeliveryStatus</returns>
        public DeliveryStatus Status(Delivery delivery);

        /// <summary>
        /// Given a delivery id and a user id, evaluates the delivery's shipping status.
        /// Returns null if there's no such delivery or user.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns>DeliveryStatus</returns>
        public DeliveryStatus Status(string deliveryId, string userId);

        /// <summary>
        /// Creates a new delivery.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="packages"></param>
        /// <param name="container"></param>
        /// <returns>Delivery</returns>
        public Delivery Create(string userId, DateTime? deliveryDate = null, List<RequestCreationOfNewPackageInNewDelivery> packages = null,
            IContainer container = null);

        /// <summary>
        /// Updates a delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="delivery"></param>
        /// <returns>Updates delivery in DB</returns>
        public void Update(string deliveryId, Delivery delivery);


        /// <summary>
        /// Updates a delivery's package list.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="packages"></param>
        /// <returns>Delivery</returns>
        public Delivery Update(string deliveryId, string userId, List<Package>? packages);

        /// <summary>
        /// Updates a delivery's delivery date.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="deliveryDate"></param>
        /// <returns>Delivery</returns>
        public Delivery Update(string deliveryId, string userId, DateTime? deliveryDate);

        /// <summary>
        /// Updates a delivery's container.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="container"></param>
        /// <returns>Delivery</returns>
        public Delivery Update(string deliveryId, string userId, IContainer container);

        /// <summary>
        /// Updates a delivery (changes it's list of packages or it's selected container and calculates it's new cost and status).
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="packages"></param>
        /// <param name="container"></param>
        /// <returns>Delivery</returns>
        public Delivery Edit(string deliveryId, string userId, DateTime? deliveryDate = null, List<Package>? packages = null, IContainer container = null);

        /// <summary>
        /// Updates a delivery list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="delivery"></param>
        /// <returns>List<Delivery></returns>
        //public List<Delivery> EditDeliveryList(List<Delivery> list, Delivery delivery);

        /// <summary>
        /// Deletes a delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns>Delivery</returns>
        public Delivery Delete(string deliveryId, string userId);

        /// <summary>
        /// Given a size of the container, return the selected container.
        /// </summary>
        /// <param name="size">IContainer</param>
        public IContainer GetContainer(ContainerSize size);

        /// <summary>
        /// Returns the container that the user have selected.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns>IContainer</returns>
        public IContainer GetContainer(string deliveryId, string userId);

        /// <summary>
        /// Given dimentions of a container, returns a new container.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="Length"></param>
        /// <returns>IContainer</returns>
        public IContainer CreateContainer(string height, string width, string Length);

        /// <summary>
        /// Given a delivery id and a user id, returns all of the packages of the delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <returns>List<Package></returns>
        public List<Package> GetAllPackages(string deliveryId, string userId);

        public List<Package> GetPackageList(string deliveryId, string userId, List<RequestCreationOfNewPackageInNewDelivery> packages);

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
        /// Adds a new package to a delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="Length"></param>
        /// <returns>Package</returns>
        public Package CreatePackage(string deliveryId, string userId, string width = null, string height = null, string Length = null);

        /// <summary>
        /// Updates a package in a delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="packageId"></param>
        /// <param name="type"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="Length"></param>
        /// <param name="weight"></param>
        /// <param name="cost"></param>
        /// <returns>Package</returns>
        public Package EditPackage(string deliveryId, string userId, string packageId,
            string width = null, string height = null, string Length = null);

        /// <summary>
        /// Updates a package in a delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="list"></param>
        /// <param name="package"></param>
        /// <returns>List<Package></returns>
        public List<Package> EditPackageList(string deliveryId, string userId, List<Package> list, Package package);

        /// <summary>
        /// Deletes a package from a delivery.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="userId"></param>
        /// <param name="packageId"></param>
        /// <returns>Package</returns>
        public Package DeletePackage(string deliveryId, string userId, string packageId);
    }
}
