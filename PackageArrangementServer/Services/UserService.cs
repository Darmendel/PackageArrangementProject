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
            return userList.Users;
        }

        public bool Exists(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;

            foreach (User user in userList.Users)
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

        public void Edit(string id, string? name = null, string? password = null)
        {
            if (Exists(id)) userList.Edit(Get(id), name, password);
        }

        public void Delete(string id)
        {
            if (Exists(id)) GetAllUsers().Remove(Get(id));
        }

        //public bool Update(string id, User user) { throw new NotImplementedException(); }

        public int CreateUser(string id, string name, string password)
        {
            if (Exists(id)) return 1;
            userList.Add(new User { Id = id, Name = name, Password = password , Deliveries = new List<Delivery>() });
            return 0;
        }

        public List<Delivery> GetAllDeliveries(string id)
        {
            if (!Exists(id)) return null;
            return deliveryService.GetAllDeliveries(id);
        }

        public Delivery GetDelivery(string userId, string deliveryId)
        {
            if (!DeliveryExists(userId, deliveryId)) return null; // redundant
            return deliveryService.Get(deliveryId, userId);
        }

        public bool DeliveryExists(string userId, string deliveryId)
        {
            if (!Exists(userId)) return false;
            return deliveryService.Exists(deliveryId, userId);
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
            User user = Get(userId);
            if (user == null) return 1;
            return 0;
            
        }

        public int DeleteDelivery(string userId, string deliveryId)
        {
            throw new NotImplementedException();
        }

        // cost and deliveryStatus might be needed to reavluate and changed.
        // maybe return a delivery instead of an int...
        public int CreateDelivery(string userId, string deliveryId, DateTime? deliveryDate = null,
            List<Package>? packages = null, Container? selectedContainer = null)
        {
            throw new NotImplementedException();
        }



    }
}
