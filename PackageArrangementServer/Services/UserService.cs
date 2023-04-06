using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class UserService : IUserService
    {
        private IDeliveryService deliveryService;
        private static UserList users = new UserList();

        public UserService(IDeliveryService ds)
        {
            this.deliveryService = ds;
        }

        public List<User> GetAllUsers()
        {
            return users.Users;
        }

        public bool Exists(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;

            foreach (var user in users.Users)
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
            if (Exists(id)) users.Edit(Get(id), name, password);
        }

        public void Delete(string id)
        {
            if (Exists(id)) GetAllUsers().Remove(Get(id));
        }

        //public bool Update(string id, User user) { throw new NotImplementedException(); }

        public int CreateUser(string id, string name, string password)
        {
            if (Exists(id)) return 1;
            users.Add(new User { Id = id, Name = name, Password = password , Deliveries = new List<Delivery>() });
            return 0;
        }

        public List<Delivery> GetAllDeliveries(string id)
        {
            User user = Get(id);
            if (user == null || user.Deliveries == null) return null;
            return user.Deliveries;
        }

        public Delivery GetDelivery(string id, string deliveryId)
        {
            //if (!DeliveryExists(id, deliveryId)) return null;
            //return GetAllDeliveries(id).Find(x => x.Id == deliveryId);
            throw new NotImplementedException();
        }

        public bool DeliveryExists(string id, string deliveryId)
        {
            List<Delivery> deliveries = GetAllDeliveries(id);
            if (deliveries == null) return false;

            foreach (Delivery delivery in deliveries)
            {
                if (delivery.Id == deliveryId) return true;
            }

            return false;
        }

        // cost and deliveryStatus might be needed to reavluate and changed.
        public int EditDelivery(string id, string deliveryId, DateTime? deliveryDate = null,
            List<Package>? packages = null, Container? selectedContainer = null)
        {
            User user = Get(id);
            if (user == null) return 1;
            return 0;
            
        }

        public int DeleteDelivery(string id, string deliveryId)
        {
            throw new NotImplementedException();
        }

        // cost and deliveryStatus might be needed to reavluate and changed.
        // maybe return a delivery instead of an int...
        public int CreateDelivery(string id, string deliveryId, DateTime? deliveryDate = null,
            List<Package>? packages = null, Container? selectedContainer = null)
        {
            throw new NotImplementedException();
        }



    }
}
