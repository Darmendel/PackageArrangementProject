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

        public int Edit(string id, string? name = null, string? password = null)
        {
            if (!Exists(id)) return 0;
            UserService.userList.Edit(Get(id), name, password);
            return 1;
        }

        public int Delete(string id)
        {
            if (!Exists(id)) return 0;
            UserService.userList.Remove(Get(id));
            return 1;
        }

        //public bool Update(string id, User user) { throw new NotImplementedException(); }

        public List<Delivery> GetAllDeliveries(string id)
        {
            if (!Exists(id)) return null;
            return deliveryService.GetAllDeliveries(id);
        }

        /**public bool DeliveryExists(string userId, string deliveryId)
        {
            if (!Exists(userId)) return false;
            return deliveryService.Exists(deliveryId, userId);
        }*/

        public Delivery GetDelivery(string userId, string deliveryId)
        {
            //if (!DeliveryExists(userId, deliveryId)) return null; // redundant
            if (!Exists(userId)) return null;
            return deliveryService.Get(deliveryId, userId);
        }

        // cost and deliveryStatus need to be eavluated.
        // maybe return the delivery (or delivery status) instead of an int...
        public int AddDelivery(string userId, DateTime? deliveryDate = null, List<Package>? packages = null,
            Container? container = null)
        {
            if (!Exists(userId)) return 0;
            throw new NotImplementedException();
        }

        public int DeliveryCost(string userId, string deliveryId)
        {
            throw new NotImplementedException();
        }

        public string DeliveryStatus(string userId, string deliveryId)
        {
            throw new NotImplementedException();
        }

        // cost and deliveryStatus might be needed to reavluate and changed.
        public int EditDelivery(string userId, string deliveryId, DateTime? deliveryDate = null,
            List<Package>? packages = null, Container? selectedContainer = null)
        {
            /**User user = Get(userId);
            if (user == null) return 0;
            return 1;*/

            throw new NotImplementedException();

        }

        public int DeleteDelivery(string userId, string deliveryId)
        {
            throw new NotImplementedException();
        }

    }
}
