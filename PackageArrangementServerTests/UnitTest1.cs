using PackageArrangementServer.Models;
using PackageArrangementServer.Models.Requests.RequestCreation;
using PackageArrangementServer.Services;

namespace PackageArrangementServerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ContainerService cs = new ContainerService();
            PackageService ps = new PackageService();
            DeliveryServiceHelper dsh = new DeliveryServiceHelper();
            DeliveryService ds = new DeliveryService(cs, ps, dsh);
            RabbitMqProducerService rs = new RabbitMqProducerService();
            UserList userList = new UserList();
            userList.Add(new User("test", null, null, null, null));
            UserService us = new UserServiceMock(ds, rs, userList);

            DateTime dateTime = DateTime.Now;
            List<RequestCreationOfNewPackageInNewDelivery> packages = new List<RequestCreationOfNewPackageInNewDelivery>();
            packages.Add(new RequestCreationOfNewPackageInNewDelivery { Width="5", Height="6", Length="7", Order="1" });
            GeneralContainer generalContainer = new GeneralContainer ("5", "6", "7" );
            RequestCreationOfNewDeliveryCustomContainer req = new RequestCreationOfNewDeliveryCustomContainer ( dateTime, packages, generalContainer );
            us.CreateDelivery("id", req);
        }
    }
}