using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IDeliveryServiceHelper
    {
        /// <summary>
        /// Given a delivery id and a user id, calculates the cost of a delivery.
        /// Returns -1 if there's no such delivery.
        /// </summary>
        /// <param name="delivery"></param>
        /// <returns>int</returns>
        int Cost(Delivery delivery);

        /// <summary>
        /// Given a delivery id and a user id, calculates the cost of a delivery.
        /// Returns -1 if there's no such delivery.
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="container"></param>
        /// <returns>int</returns>
        int Cost(List<Package> packages = null, IContainer? container = null);
    }
}