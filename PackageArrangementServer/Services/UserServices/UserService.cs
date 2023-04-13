using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class UserService : IUserService
    {
        private IDeliveryService deliveryService;
        private static UserList userList;

        public UserService(IDeliveryService ds)
        {
            this.deliveryService = ds;
            //userList = new UserList();
            userList = StaticData.GetUsers();
        }

        public List<User> GetAllUsers()
        {
            if (userList.Count == 0) return null;
            return UserService.userList.Users;
        }

        public bool Exists(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;

            foreach (User user in UserService.userList.Users)
            {
                if (user.Id == id) return true;
            }
            return false;
        }

        public User Get(string id)
        {
            if (!Exists(id)) return null;
            return GetAllUsers().Find(x => x.Id == id);
        }

        public User Create(string id, string name, string email, string password)
        {
            if (Exists(id)) return null;
            User user = new User { Id = id, Name = name, Email = email, Password = password, Deliveries = new List<Delivery>() };
            userList.Add(user);
            return user;
        }

        public User Create(string id, string name, string email, string password, List<Delivery> deliveries = null)
        {
            if (Exists(id)) return null;
            User user = null;

            if (deliveries == null) user = new User {Id = id, Name = name, Email = email, Password = password, Deliveries = new List<Delivery>() };
            else user = new User { Id = id, Name = name, Email = email, Password = password, Deliveries = deliveries };

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

        public int CreateDelivery(string userId, DateTime? deliveryDate = null, List<RequestCreationOfNewPackage>? packages = null,
            IContainer container = null)
        {
            if (!Exists(userId)) return 0;

            Delivery delivery = deliveryService.Create(userId, deliveryDate, packages, container);
            if (delivery == null) return 0;

            //return Update(userId, delivery, "add");
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

        public int UpdateDelivery(string userId, string deliveryId, IContainer container)
        {
            if (!Exists(userId)) return 0;

            Delivery delivery = deliveryService.Update(userId, deliveryId, container);
            if (delivery == null) return 0;
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

        public int CreatePackage(string userId, string deliveryId, string type = null, string amount = null,
            string width = null, string height = null, string depth = null, string weight = null,
            string cost = null, string address = null)
        {
            if (!Exists(userId)) return 0;

            Package package = deliveryService.CreatePackage(deliveryId, userId, type, amount, width, height,
                depth, weight, cost, address);

            if (package == null) return 0;
            return 1;
        }

        public int EditPackage(string userId, string deliveryId, string packageId, string type = null,
            string amount = null, string width = null, string height = null, string depth = null, string weight = null,
            string cost = null, string address = null)
        {
            if (!Exists(userId)) return 0;

            Package package = deliveryService.EditPackage(deliveryId, userId, packageId, type, amount, width, height,
                depth, weight, cost, address);

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
