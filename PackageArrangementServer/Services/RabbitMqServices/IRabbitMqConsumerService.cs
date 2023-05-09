using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IRabbitMqConsumerService
    {
        /// <summary>
        /// Returns a list of packages organized by package arrangement algorithm.
        /// </summary>
        /// <param name="packages"></param>
        /// <returns>List<Package></returns>
        public List<Package> DisplayDelivery(List<Package> packages);
    }
}
