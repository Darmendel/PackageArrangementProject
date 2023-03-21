using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        /// <returns>List<User></returns>
        public List<User> GetAllUsers();

        /// <summary>
        /// Checks if a user exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public bool Exists(string id);

        /// <summary>
        /// Returns a user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        public User Get(string id);

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="password"></param>
        public void Edit(string id, string? name = null, string? password = null);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id);

        /// <summary>
        /// Creates a new user. Returns 1 if succeeded and 0 otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns>int</returns>
        public int CreateUser(string id, string name, string password);

        /// <summary>
        /// Returns a list of a user's past (and current) deliveries.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<Delivery></returns>
        public List<Delivery> GetAllDeliveries(string id);

        /// <summary>
        /// Returns a user's delivery by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deliveryId"></param>
        /// <returns>Delivery</returns>
        public Delivery GetDelivery(string id, string deliveryId);

        /// <summary>
        /// Checks if a user have made a certain delivery before.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deliveryId"></param>
        /// <returns>bool</returns>
        public bool DeliveryExists(string id, string deliveryId);

        /// <summary>
        /// Updates a user's delivery. Returns 1 if succeeded and 0 otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deliveryId"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="packages"></param>
        /// <param name="selectedContainer"></param>
        /// <returns>int</returns>
        public int EditDelivery(string id, string deliveryId, DateTime? deliveryDate = null,
            List<Package>? packages = null, Container? selectedContainer = null);

        /// <summary>
        /// Deletes a user's delivery. Returns 1 if succeeded and 0 otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deliveryId"></param>
        /// <returns>int</returns>
        public int DeleteDelivery(string id, string deliveryId);

        /// <summary>
        /// Creates a new delivery for a user. Returns 1 if succeeded and 0 otherwise.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deliveryId"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="packages"></param>
        /// <param name="selectedContainer"></param>
        /// <returns>int</returns>
        public int CreateDelivery(string id, string deliveryId, DateTime? deliveryDate = null,
            List<Package>? packages = null, Container? selectedContainer = null);
    }
}
