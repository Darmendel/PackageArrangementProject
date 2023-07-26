using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class UserServiceMock : UserService
    {
        private static UserList userList = new UserList();

        public UserServiceMock(IDeliveryService ds, IRabbitMqProducerService ps, UserList ul) : base(ds, ps)
        {
            base.SetUserList(ul);
        }
        public UserServiceMock(IDeliveryService ds, IRabbitMqProducerService ps) : base(ds, ps)
        {
            base.SetUserList(new UserList());
        }
    }
}