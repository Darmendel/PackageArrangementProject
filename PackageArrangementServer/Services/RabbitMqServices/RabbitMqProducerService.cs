using PackageArrangementServer.Models;
using PackageArrangementServer.Models.Requests.RequestCreation;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PackageArrangementServer.Services
{
    public class RabbitMqProducerService : IRabbitMqProducerService
    {
        private RabbitMqProducer producer;

        public RabbitMqProducerService() { producer = new RabbitMqProducer(); }

        /// <summary>
        /// Sends a message to the rabbitMQ queue.
        /// Returns 1 is succeeded, and 0 otherwise.
        /// </summary>
        /// <param name="deliveryRequest"></param>
        /// <param name="friendqueue"></param>
        /// <returns>int</returns>
        public int Send(DeliveryRequest deliveryRequest, string friendqueue)
        {
            
            JsonSerializerOptions options = new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };

            string message = JsonSerializer.Serialize<DeliveryRequest>(deliveryRequest, options);
            bool res = producer.Send(message, friendqueue);
            return res ? 1 : 0;
        }
    }
}
