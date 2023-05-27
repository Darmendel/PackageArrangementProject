using PackageArrangementServer.Models;
using System.Text.Json;

namespace PackageArrangementServer.Services
{
    public class RabbitMqProducerService : IRabbitMqProducerService
    {
        private RabbitMqProducer producer;

        public RabbitMqProducerService() { producer = new RabbitMqProducer(); }

        public int Send(string deliveryId, List<Package> packages, IContainer container, string friendqueue)
        {
            if (packages == null || container == null || friendqueue == null) return 0;

            string message = JsonSerializer.Serialize(packages);
            string c = JsonSerializer.Serialize(container);
            message += "," + c;

            // Console.WriteLine(message);

            bool res = producer.Send(message, friendqueue);
            return res ? 1 : 0;
        }
    }
}
