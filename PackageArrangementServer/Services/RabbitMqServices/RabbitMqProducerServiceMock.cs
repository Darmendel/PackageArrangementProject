using PackageArrangementServer.Models.Requests.RequestCreation;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace PackageArrangementServer.Services.RabbitMqServices
{
    public class RabbitMqProducerServiceMock : IRabbitMqProducerService
    {

        public RabbitMqProducerServiceMock() { }

        public int Send(DeliveryRequest deliveryRequest, string friendqueue)
        {

            return 1;
        }
    }
}
