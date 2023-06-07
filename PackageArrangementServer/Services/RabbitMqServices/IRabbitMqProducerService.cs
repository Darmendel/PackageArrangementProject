using PackageArrangementServer.Models;
using RabbitMQ.Client;

namespace PackageArrangementServer.Services
{
    public interface IRabbitMqProducerService
    {
        /// <summary>
        /// Sends a message to the rabbitMQ queue.
        /// Returns 1 is succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="container"></param>
        /// <param name="friendqueue"></param>
        /// <returns>int</returns>
        public int Send(string deliveryId, List<Package> packages, IContainer container, string friendqueue);
    }
}
