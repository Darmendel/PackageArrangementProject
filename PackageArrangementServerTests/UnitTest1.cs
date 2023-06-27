using PackageArrangementServer;
using PackageArrangementServer.Models;
using PackageArrangementServer.Models.Requests.RequestCreation;
using PackageArrangementServer.Services;
using PackageArrangementServer.Services.RabbitMqServices;

namespace PackageArrangementServerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        /*
         * Test that asserts that a custom container is built
         * according to the given requirements.
         * Asserts that the delivery is created.
         * Epic #1, User story #4 & #5
         */
        public void TestMethod1()
        {
            ContainerService cs = new ContainerService();
            PackageService ps = new PackageService();
            DeliveryServiceHelper dsh = new DeliveryServiceHelper();
            DeliveryService ds = new DeliveryService(cs, ps, dsh);
            RabbitMqProducerServiceMock rs = new RabbitMqProducerServiceMock();
            UserList userList = new UserList();
            userList.Add(new User("test", null, null, null, new List<Delivery>()));
            UserService us = new UserServiceMock(ds, rs, userList);

            DateTime dateTime = DateTime.Now;
            List<RequestCreationOfNewPackageInNewDelivery> packages = new List<RequestCreationOfNewPackageInNewDelivery>
            {
                new RequestCreationOfNewPackageInNewDelivery { Width = "5", Height = "6", Length = "7", Order = "1" }
            };
            GeneralContainer generalContainer = new GeneralContainer ("5", "6", "7" );
            RequestCreationOfNewDeliveryCustomContainer req = new RequestCreationOfNewDeliveryCustomContainer ( dateTime, packages, generalContainer );
            string deliveryId = us.CreateDelivery("test", req);
            Assert.IsNotNull( deliveryId );
            Delivery delivery = us.GetDelivery("test", deliveryId);
            Assert.IsNotNull( delivery );
            Assert.AreEqual(generalContainer, delivery.Container);
        }
    }
}