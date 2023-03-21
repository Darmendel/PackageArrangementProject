using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public bool Exists(string id);
        public User Get(string id);
        public void Edit(string id, string? name = null, string? password = null);
        public void Delete(string id);
        public int CreateUser(string id, string name, string password);
        public List<Delivery> GetAllDeliveries(string id);
        public Delivery GetDelivery(string id, string deliveryId);
        public bool DeliveryExists(string id, string deliveryId);
        public int EditDelivery(string id, string deliveryId, DateTime? deliveryDate = null, List<Package>? packages = null, Container? selectedContainer = null);
        public int DeleteDelivery(string id, string deliveryId);
        public int CreateDelivery(string id, string deliveryId, DateTime? deliveryDate = null, List<Package>? packages = null, Container? selectedContainer = null);
    }
}
