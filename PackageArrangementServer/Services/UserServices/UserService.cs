using PackageArrangementServer.Models;
using Firebase.Database;
using Firebase.Database.Query;
using PackageArrangementServer.Models.Requests.RequestCreation;

namespace PackageArrangementServer.Services
{
    public class UserService : IUserService
    {
        private IDeliveryService deliveryService;
        private IRabbitMqProducerService producerService;
        private static FirebaseClient client = new FirebaseClient("https://packagearrangementprojectbiu-default-rtdb.europe-west1.firebasedatabase.app/");
        private static UserList userList = FetchFromDB().Result;

        public UserService(IDeliveryService ds, IRabbitMqProducerService ps)
        {
            deliveryService = ds;
            producerService = ps;
            //userList = new UserList();
            //userList = StaticData.GetUsers();
        }
        
        private static async Task<UserList> FetchFromDB()
        {
            var users = await client.Child("Users/").OnceAsync<RegisterRequest>();
            List<User> usersList = new List<User>();

            foreach (var us in users) 
                usersList.Add(new User(us.Key, us.Object, DeliveryService.GetAllDeliveries(us.Key)));
         
            return new UserList(usersList);

        }

        private async Task<string> AddToDB(RegisterRequest request)
        {
            try { return (await client.Child("Users").PostAsync(request)).Key; }
            catch (Exception ex) { return null;}
        }

        public void SetUserList(UserList userList)
        {
            UserService.userList = userList;
        }


        /// <summary>
        /// Adds new user to the db.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>bool</returns>
        public bool SignUpUser(RegisterRequest request)
        {
            if (request == null) return false;
            if (Exists(request.Email, "email")) return false;
            string key = AddToDB(request).Result;
            if (key != null) userList.Add(new User(key, request));
            else return false;

            return true;
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>bool</returns>
        public string Login(LoginRequest request)
        {
            if (request == null) return null;
            User user = Get(request.Email, "email");
            if (user != null && user.Password.Equals(request.Password))
                return user.Id;
            return null;
        }

        public List<User> GetAllUsers()
        {
            //if (userList.Count == 0) return null;
            return UserService.userList.Users; // Returns empty lists as well
        }

        public bool Exists(string val, string type)
        {
            if (string.IsNullOrEmpty(val)) return false;

            foreach (User user in UserService.userList.Users)
            {
                if (type == "email")
                    if (user.Email == val) return true;
                if (type == "id")
                    if (user.Id == val) return true;
            }
            return false;
        }

        public User Get(string val, string type)
        {
            if (!Exists(val, type)) return null;
            return type == "email" ? GetAllUsers().Find(x => x.Email == val) : 
                                     GetAllUsers().Find(x => x.Id == val);
        }

        public User Create(string id, string name, string email, string password)
        {
            if (Exists(id, "email")) return null;
            User user = new (id, name, email, password, new List<Delivery>() );
            userList.Add(user);
            return user;
        }

        public User Create(string id, string name, string email, string password, List<Delivery> deliveries = null)
        {
            if (Exists(id, "email")) return null;
            User user = null;

            if (deliveries == null) user = new User (id, name, email, password, new List<Delivery>() );
            else user = new User (id, name, email, password, deliveries );

            userList.Add(user);
            return user;
        }

        public int Edit(string id, string name = null, string email = null, string password = null,
            List<Delivery> deliveries = null)
        {
            if (!Exists(id, "id")) return 0;
            UserService.userList.Edit(Get(id, "id"), name, email, password, deliveries);
            return 1;
        }

        public int Delete(string id)
        {
            if (!Exists(id, "id")) return 0;
            UserService.userList.Remove(Get(id, "id"));
            return 1;
        }

        public List<Delivery> GetAllDeliveries(string id)
        {
            if (!Exists(id, "id")) return null;
            return DeliveryService.GetAllDeliveries(id);
        }

        public Delivery GetDelivery(string userId, string deliveryId)
        {
            return deliveryService.Get(deliveryId, userId);
        }

        /*private int Update(string userId, Delivery delivery, string op)
        {
            User user = Get(userId);
            if (user == null) return 0;
            
            List<Delivery> dList = GetAllDeliveries(userId);
            if (dList == null) return 0;

            if (op.Equals("add")) dList.Add(delivery);
            else if (op.Equals("edit")) dList = deliveryService.EditDeliveryList(dList, delivery);
            else if (op.Equals("delete")) dList.Remove(delivery);
            else return 0;

            UserService.userList.Edit(user, deliveries: dList);
            return 1;
        }*/

        private string CreateDeliveryGeneral(string userId, DateTime? deliveryDate, List<RequestCreationOfNewPackageInNewDelivery>? packages,
            IContainer container)
        {
            User user = Get(userId, "id");
            if (user == null) return null;

            Delivery delivery = deliveryService.Create(userId, deliveryDate, packages, container);
            if (delivery == null) return null;

            DeliveryRequest deliveryRequest = new DeliveryRequest(delivery.Id, container, delivery.FirstPackages, userId);

            int res = producerService.Send(deliveryRequest, "order_report"); // change null to name of queue
            if (res == 0) return null;

            //return Update(userId, delivery, "add");
            userList.AddDelivery(user, delivery);
            return delivery.Id;
        }
        public string CreateDelivery(string userId, DateTime? deliveryDate = null, List<RequestCreationOfNewPackageInNewDelivery>? packages = null,
            ContainerSize size = ContainerSize.Large)
        {
            IContainer container;

            switch (size)
            {
                case ContainerSize.Small:
                    container = new SmallContainer();
                    break;
                case ContainerSize.Medium:
                    container = new MediumContainer();
                    break;
                case ContainerSize.Large:
                    container = new BigContainer();
                    break;
                default:
                    container = new BigContainer();
                    break;

            }
            return CreateDeliveryGeneral(userId, deliveryDate, packages, container);
        }


        public string CreateDelivery(string userId, RequestCreationOfNewDeliveryCustomContainer req)
        {
            return CreateDeliveryGeneral(userId, req.DeliveryDate, req.Packages, req.Container);
        }

        public int GetDeliveryCost(string userId, string deliveryId)
        {
            if (!Exists(userId, "id")) return -1;
            return deliveryService.Cost(deliveryId, userId);
        }

        public DeliveryStatus GetDeliveryStatus(string userId, string deliveryId)
        {
            if (!Exists(userId, "id")) return DeliveryStatus.NonExisting;
            return deliveryService.Status(deliveryId, userId);
        }

        public int UpdateDelivery(string userId, string deliveryId, List<Package>? packages)
        {
            if (!Exists(userId, "id")) return 0;

            Delivery delivery = deliveryService.Update(userId, deliveryId, packages);
            if (delivery == null) return 0;

            IContainer container = deliveryService.GetContainer(deliveryId, userId);
            if (container == null) return 0;

            DeliveryRequest deliveryRequest = new DeliveryRequest(delivery.Id, container, packages, userId);

            int res = producerService.Send(deliveryRequest, "order_report"); // change null to name of queue
            if (res == 0) return 0;

            return 1;
        }

        public int UpdateDelivery(string userId, string deliveryId, DateTime? deliveryDate)
        {
            if (!Exists(userId, "id")) return 0;

            Delivery delivery = deliveryService.Update(userId, deliveryId, deliveryDate);
            if (delivery == null) return 0;
            return 1;
        }

        public int UpdateDelivery(string userId, string deliveryId, IContainer container)
        {
            if (!Exists(userId, "id")) return 0;

            Delivery delivery = deliveryService.Update(userId, deliveryId, container);
            if (delivery == null) return 0;

            List<Package> packages = deliveryService.GetAllPackages(deliveryId, userId);
            if (packages == null) return 0;

            DeliveryRequest deliveryRequest = new DeliveryRequest(delivery.Id, container, packages, userId);

            int res = producerService.Send(deliveryRequest, "order_report"); // change null to name of queue
            if (res == 0) return 0;

            return 1;
        }

        // cost and deliveryStatus might be needed to reavluate and changed.
        public int EditDelivery(string userId, string deliveryId, DateTime? deliveryDate = null,
            List<Package>? packages = null, IContainer container = null)
        {
            if (!Exists(userId, "id")) return 0;

            Delivery delivery = deliveryService.Edit(deliveryId, userId, deliveryDate, packages, container);
            if (delivery == null) return 0;

            //return Update(userId, delivery, "edit");
            return 1;
        }

        public int DeleteDelivery(string userId, string deliveryId)
        {
            if (!Exists(userId, "id")) return 0;

            Delivery delivery = deliveryService.Delete(deliveryId, userId);
            if (delivery == null) return 0;

            //return Update(userId, delivery, "delete");
            return 1;
        }

        public IContainer GetContainer(ContainerSize size)
        {
            return deliveryService.GetContainer(size);
        }

        public IContainer CreateContainer(string height, string width, string Length)
        {
            return deliveryService.CreateContainer(height, width, Length);
        }

        public List<Package> GetAllPackages(string userId, string deliveryId)
        {
            if (!Exists(userId, "id")) return null;
            return deliveryService.GetAllPackages(deliveryId, userId);
        }

        public Package GetPackage(string userId, string deliveryId, string packageId)
        {
            if (!Exists(userId, "id")) return null;
            return deliveryService.GetPackage(deliveryId, userId, packageId);
        }

        public int CreatePackage(string userId, string deliveryId, string width = null,
            string height = null, string Length = null)
        {
            if (!Exists(userId, "id")) return 0;

            Package package = deliveryService.CreatePackage(deliveryId, userId, width, height, Length);

            if (package == null) return 0;
            return 1;
        }

        public int EditPackage(string userId, string deliveryId, string packageId, 
            string width = null, string height = null, string Length = null)
        {
            if (!Exists(userId, "id")) return 0;

            Package package = deliveryService.EditPackage(deliveryId, userId, packageId, width, height, Length);

            if (package == null) return 0;
            return 1;
        }

        public int DeletePackage(string userId, string deliveryId, string packageId)
        {
            if (!Exists(userId, "id")) return 0;
            Package package = deliveryService.DeletePackage(deliveryId, userId, packageId);

            if (package == null) return 0;
            return 1;
        }

        public User FindUserByDeliveryId(string deliveryId)
        {
            foreach (User u in GetAllUsers()) {
                foreach (Delivery d in u.Deliveries)
                {
                    if (d.Id.Equals(deliveryId))
                    {
                        return u;
                    }
                }
            }
            return null;
        }
    }
}
