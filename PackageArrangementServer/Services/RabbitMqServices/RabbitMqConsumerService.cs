using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class RabbitMqConsumerService : IRabbitMqConsumerService
    {
        private IUserService userService;

        public RabbitMqConsumerService(IUserService us) { userService = us; }

        public List<Package> DisplayDelivery(List<Package> packages)
        {
            throw new NotImplementedException();
        }
    }
}
