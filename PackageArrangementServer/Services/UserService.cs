using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class UserService : IUserService
    {
        private IDeliveryService deliveryService;
        private static UserList userList = new UserList();

        public UserService(IDeliveryService ds)
        {
            this.deliveryService = ds;
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

        public int Add(string id, string name, string password)
        {
            if (Exists(id)) return 0;
            userList.Add(new User { Id = id, Name = name, Password = password, Deliveries = new List<Delivery>() });
            return 1;
        }

        public int Edit(string id, string name = null, string password = null, List<Delivery> deliveries = null)
        {
            if (!Exists(id)) return 0;
            UserService.userList.Edit(Get(id), name, password, deliveries);
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

        // cost and deliveryStatus need to be eavluated.
        // maybe return the delivery (or delivery status) instead of an int...
        public int AddDelivery(string userId, DateTime? deliveryDate = null, List<Package>? packages = null,
            Container? container = null)
        {
            if (!Exists(userId)) return 0;
            return deliveryService.Add(userId, deliveryDate, packages, container);
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

        // cost and deliveryStatus might be needed to reavluate and changed.
        public int EditDelivery(string userId, string deliveryId, DateTime? deliveryDate = null,
            List<Package>? packages = null, Container? selectedContainer = null)
        {
            if (!Exists(userId)) return 0;
            return deliveryService.Edit(deliveryId, userId, deliveryDate, packages, selectedContainer);

        }

        public int DeleteDelivery(string userId, string deliveryId)
        {
            if (!Exists(userId)) return 0;
            return deliveryService.Delete(deliveryId, userId);
        }

    }
}
