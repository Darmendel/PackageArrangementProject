using PackageArrangementServer.Models;
using Firebase.Database;
using Firebase.Database.Query;

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
            this.deliveryService = ds;
            this.producerService = ps;
            //userList = new UserList();
            //userList = StaticData.GetUsers();
        }
        
        private static async Task<UserList> FetchFromDB()
        {
            var users = await client.Child("Users/").OnceAsync<RegisterRequest>();
            List<User> usersList = new List<User>();

            foreach (var us in users) 
                usersList.Add(new User(us.Key, us.Object));
         
            return new UserList(usersList);

        }

        private async Task<string> AddToDB(RegisterRequest request)
        {
            try { return (await client.Child("Users").PostAsync(request)).Key; }
            catch (Exception ex) { return null;}
        }


        /// <summary>
        /// Adds new user to the db.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>bool</returns>
        public bool SignUpUser(RegisterRequest request)
        {
            if (request == null) return false;
            if (Exists(request.Email)) return false;
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
        public bool Login(LoginRequest request)
        {
            if (request == null) return false;
            User user = Get(request.Email);
            if (user != null) return user.Password.Equals(request.Password);
            return false;
        }

        public List<User> GetAllUsers()
        {
            //if (userList.Count == 0) return null;
            return UserService.userList.Users; // Returns empty lists as well
        }

        public bool Exists(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            foreach (User user in UserService.userList.Users)
            {
                if (user.Email == email) return true;
            }
            return false;
        }

        public User Get(string email)
        {
            if (!Exists(email)) return null;
            return GetAllUsers().Find(x => x.Email == email);
        }

        public User Create(string id, string name, string email, string password)
        {
            if (Exists(id)) return null;
            User user = new (id, name, email, password, new List<Delivery>() );
            userList.Add(user);
            return user;
        }

        public User Create(string id, string name, string email, string password, List<Delivery> deliveries = null)
        {
            if (Exists(id)) return null;
            User user = null;

            if (deliveries == null) user = new User (id, name, email, password, new List<Delivery>() );
            else user = new User (id, name, email, password, deliveries );

            userList.Add(user);
            return user;
        }

        public int Edit(string id, string name = null, string email = null, string password = null,
            List<Delivery> deliveries = null)
        {
            if (!Exists(id)) return 0;
            UserService.userList.Edit(Get(id), name, email, password, deliveries);
            return 1;
        }

        public int Delete(string id)
        {
            if (!Exists(id)) return 0;
            UserService.userList.Remove(Get(id));
            return 1;
        }

        public List<Delivery> GetAllDeliveries(string id)
        {
            if (!Exists(id)) return null;
            return deliveryService.GetAllDeliveries(id);
        }

        public Delivery GetDelivery(string userId, string deliveryId)
        {
            if (!Exists(userId)) return null;
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

        public int CreateDelivery(string userId, DateTime? deliveryDate = null, List<RequestCreationOfNewPackageInNewDelivery>? packages = null,
            IContainer container = null)
        {
            if (container == null)
            {
                container = new BigContainer();
            }
            User user = Get(userId);
            if (user == null) return 0;

            Delivery delivery = deliveryService.Create(userId, deliveryDate, packages, container);
            if (delivery == null) return 0;

            List<Package> packageList = deliveryService.GetPackageList(delivery.Id, userId, packages);
            if (packageList == null) return 0;

            int res = producerService.Send(delivery.Id, packageList, container, "order_report"); // change null to name of queue
            if (res == 0) return 0;

            //return Update(userId, delivery, "add");
            userList.AddDelivery(user, delivery);
            return 1;
        }

        public int GetDeliveryCost(string userId, string deliveryId)
        {
            if (!Exists(userId)) return -1;
            return deliveryService.Cost(deliveryId, userId);
        }

        public DeliveryStatus GetDeliveryStatus(string userId, string deliveryId)
        {
            if (!Exists(userId)) return DeliveryStatus.NonExisting;
            return deliveryService.Status(deliveryId, userId);
        }

        public int UpdateDelivery(string userId, string deliveryId, List<Package>? packages)
        {
            if (!Exists(userId)) return 0;

            Delivery delivery = deliveryService.Update(userId, deliveryId, packages);
            if (delivery == null) return 0;

            IContainer container = deliveryService.GetContainer(deliveryId, userId);
            if (container == null) return 0;

            int res = producerService.Send(delivery.Id, packages, container, null); // change null to name of queue
            if (res == 0) return 0;

            return 1;
        }

        public int UpdateDelivery(string userId, string deliveryId, DateTime? deliveryDate)
        {
            if (!Exists(userId)) return 0;

            Delivery delivery = deliveryService.Update(userId, deliveryId, deliveryDate);
            if (delivery == null) return 0;
            return 1;
        }

        public int UpdateDelivery(string userId, string deliveryId, IContainer container)
        {
            if (!Exists(userId)) return 0;

            Delivery delivery = deliveryService.Update(userId, deliveryId, container);
            if (delivery == null) return 0;

            List<Package> packages = deliveryService.GetAllPackages(deliveryId, userId);
            if (packages == null) return 0;

            int res = producerService.Send(delivery.Id, packages, container, null); // change null to name of queue
            if (res == 0) return 0;

            return 1;
        }

        // cost and deliveryStatus might be needed to reavluate and changed.
        public int EditDelivery(string userId, string deliveryId, DateTime? deliveryDate = null,
            List<Package>? packages = null, IContainer container = null)
        {
            if (!Exists(userId)) return 0;

            Delivery delivery = deliveryService.Edit(deliveryId, userId, deliveryDate, packages, container);
            if (delivery == null) return 0;

            //return Update(userId, delivery, "edit");
            return 1;
        }

        public int DeleteDelivery(string userId, string deliveryId)
        {
            if (!Exists(userId)) return 0;

            Delivery delivery = deliveryService.Delete(deliveryId, userId);
            if (delivery == null) return 0;

            //return Update(userId, delivery, "delete");
            return 1;
        }

        public IContainer GetContainer(ContainerSize size)
        {
            return deliveryService.GetContainer(size);
        }

        public IContainer CreateContainer(string height, string width, string depth)
        {
            return deliveryService.CreateContainer(height, width, depth);
        }

        public List<Package> GetAllPackages(string userId, string deliveryId)
        {
            if (!Exists(userId)) return null;
            return deliveryService.GetAllPackages(deliveryId, userId);
        }

        public Package GetPackage(string userId, string deliveryId, string packageId)
        {
            if (!Exists(userId)) return null;
            return deliveryService.GetPackage(deliveryId, userId, packageId);
        }

        public int CreatePackage(string userId, string deliveryId, string amount = null, string width = null,
            string height = null, string depth = null, string address = null)
        {
            if (!Exists(userId)) return 0;

            Package package = deliveryService.CreatePackage(deliveryId, userId, amount, width, height, depth, address);

            if (package == null) return 0;
            return 1;
        }

        public int EditPackage(string userId, string deliveryId, string packageId, string amount = null,
            string width = null, string height = null, string depth = null, string address = null)
        {
            if (!Exists(userId)) return 0;

            Package package = deliveryService.EditPackage(deliveryId, userId, packageId, amount, width, height, depth, address);

            if (package == null) return 0;
            return 1;
        }

        public int DeletePackage(string userId, string deliveryId, string packageId)
        {
            if (!Exists(userId)) return 0;
            Package package = deliveryService.DeletePackage(deliveryId, userId, packageId);

            if (package == null) return 0;
            return 1;
        }

    }
}
