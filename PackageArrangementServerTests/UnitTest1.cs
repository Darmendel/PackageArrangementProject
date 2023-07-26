using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PackageArrangementServer;
using PackageArrangementServer.Controllers;
using PackageArrangementServer.Models;
using PackageArrangementServer.Models.Requests.RequestCreation;
using PackageArrangementServer.Services;
using PackageArrangementServer.Services.RabbitMqServices;

namespace PackageArrangementServerTests
{
    [TestClass]
    public class UnitTest1
    {
        private ContainerService cs;
        private PackageService ps;
        private DeliveryServiceHelper dsh;
        private DeliveryService ds;
        private RabbitMqProducerServiceMock rs;
        UserService us;

        public UnitTest1()
        {
            cs = new ContainerService();
            ps = new PackageService();
            dsh = new DeliveryServiceHelper();
            ds = new DeliveryService(cs, ps, dsh);
            rs = new RabbitMqProducerServiceMock();
        }

        [TestMethod]
        public void SignUpValidRequestReturnsCreatedStatus()
        {
            RegisterRequest req = new RegisterRequest("Dar@mail", "Mend", "Password");
            us = new UserServiceMock(ds, rs);
            UserController userController = new UserController(us);
            userController.ControllerContext = new ControllerContext();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();

            userController.Post(req);

            Assert.AreEqual(201, userController.Response.StatusCode);
        }

        [TestMethod]
        public void SignUpValidRequestReturnsBadRequestStatus()
        {
            RegisterRequest req = new RegisterRequest("Dar@mail", "Mend", "Password");
            us = new UserServiceMock(ds, rs);
            UserController userController = new UserController(us);
            userController.ControllerContext = new ControllerContext();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();

            userController.Post(req);
            userController.Post(req);

            Assert.AreEqual(400, userController.Response.StatusCode);
        }
        [TestMethod]
        public void LoginValidCredentialsReturnsUserId()
        {
            RegisterRequest req = new RegisterRequest("Mend", "Dar@mail", "Password");
            UserList userList = new UserList();
            userList.Add(new User("test", req, new List<Delivery>()));
            us = new UserServiceMock(ds, rs, userList);
            UserController userController = new UserController(us);

            userController.ControllerContext = new ControllerContext();
            userController.ControllerContext.HttpContext = new DefaultHttpContext();
            us.SetUserList(userList);
            LoginRequest loginRequest = new LoginRequest("Dar@mail", "Password");
            var result = userController.Post(loginRequest);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, userController.Response.StatusCode);
        }

        public void LoginValidCredentialsReturnsUnauthorizedStatus()
        {
            RegisterRequest req = new RegisterRequest("Dar@mail", "Mend", "Password");
            UserList userList = new UserList();
            userList.Add(new User("test", req, new List<Delivery>()));
            us = new UserServiceMock(ds, rs, userList);
            UserController userController = new UserController(us);
            us.SetUserList(userList);

            LoginRequest loginRequest = new LoginRequest("Dar@mail", "Password1");
            var result = userController.Post(loginRequest);
            Assert.IsNull(result);
            Assert.AreEqual(401, userController.Response.StatusCode);
        }


        [TestMethod]
        /*
         * Test that asserts that a custom container is built
         * according to the given requirements.
         * Asserts that the delivery is created.
         * Epic #1, User story #4 & #5
         */
        public void TestMethod1()
        {
            UserList userList = new UserList();
            userList.Add(new User("test", null, null, null, new List<Delivery>()));
            UserService us = new UserServiceMock(ds, rs, userList);

            DateTime dateTime = DateTime.Now;
            List<RequestCreationOfNewPackageInNewDelivery> packages = new List<RequestCreationOfNewPackageInNewDelivery>
            {
                new RequestCreationOfNewPackageInNewDelivery { Width = "5", Height = "6", Length = "7", Order = "1" }
            };
            GeneralContainer generalContainer = new GeneralContainer("5", "6", "7");
            RequestCreationOfNewDeliveryCustomContainer req = new RequestCreationOfNewDeliveryCustomContainer(dateTime, packages, generalContainer);
            string deliveryId = us.CreateDelivery("test", req);
            Assert.IsNotNull(deliveryId);
            Delivery delivery = us.GetDelivery("test", deliveryId);
            Assert.IsNotNull(delivery);
            Assert.AreEqual(generalContainer, delivery.Container);
        }
    }
}