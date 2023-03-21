using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class UserService : IUserService
    {
        public List<User> GetAllUsers() { throw new NotImplementedException(); }

        public bool Exists(string id) { throw new NotImplementedException(); }
        public User Get(string id) { throw new NotImplementedException(); }
        public void Edit(string id, string? name = null, string? password = null) { throw new NotImplementedException(); }
        public void Delete(string id) { throw new NotImplementedException(); }
        //public bool Update(string id, User user) { throw new NotImplementedException(); }
        public int CreateUser(string id, string name, string password) { throw new NotImplementedException(); }
        public List<Delivery> GetAllDeliveries(string id) { throw new NotImplementedException(); }
        public Delivery GetDelivery(string id, string deliveryId) { throw new NotImplementedException(); }
        public bool DeliveryExists(string id, string deliveryId) { throw new NotImplementedException(); }

        // cost and deliveryStatus might be needed to reavluate and changed.
        public int EditDelivery(string id, string deliveryId, DateTime? deliveryDate = null, List<Package>? packages = null, Container? selectedContainer = null) { throw new NotImplementedException(); }
        public int DeleteDelivery(string id, string deliveryId) { throw new NotImplementedException(); }

        // cost and deliveryStatus might be needed to reavluate and changed.
        public int CreateDelivery(string id, string deliveryId, DateTime? deliveryDate = null, List<Package>? packages = null, Container? selectedContainer = null) { throw new NotImplementedException(); }



    }
}
