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

        public int Send(string deliveryId, List<Package> packages, IContainer container, string friendqueue)
        {
            if (packages == null || container == null || friendqueue == null) return 0;

            DeliveryRequest deliveryRequest = new DeliveryRequest(deliveryId, container, packages);
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
