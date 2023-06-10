using PackageArrangementServer.Models;
using PackageArrangementServer.Models.Requests.RequestCreation;
using RabbitMQ.Client;

namespace PackageArrangementServer.Services
{
    public interface IRabbitMqProducerService
    {
        /// <summary>
        /// Sends a message to the rabbitMQ queue.
        /// Returns 1 is succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="deliveryRequest"></param>
        /// <param name="friendqueue"></param>
        /// <returns>int</returns>
        public int Send(DeliveryRequest deliveryRequest, string friendqueue);
    }
}
