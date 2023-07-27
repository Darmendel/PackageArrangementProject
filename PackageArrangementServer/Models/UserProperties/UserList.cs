namespace PackageArrangementServer.Models
{
    public class UserList
    {
        private List<User> _users;

        public UserList(List<User> users)
        {
            _users = new List<User>();
            foreach (User user in users)
            {
                _users.Add(user);
            }
        }

        public UserList()
        {
            _users = new List<User>();
        }

        public List<User> Users { get { return _users; } }

        public int Count { get { return _users.Count; } }

        public void Add(User user)
        {
            if (user == null || _users.Contains(user)) return;
            _users.Add(user);
        }

        public void Edit(User user, string name = null, string email = null, string password = null, List<Delivery> deliveries = null)
        {
            if (user == null) return;
            if (_users.Contains(user))
            {
                int index = _users.IndexOf(user);

                if (name != null) _users[index].Name = name;
                if (email != null) _users[index].Email = email;
                if (password != null) _users[index].Password = password;
                if (deliveries != null) _users[index].Deliveries = deliveries;
            }
        }

        public void Remove(User user)
        {
            if (_users.Contains(user)) _users.Remove(user);
        }

        public void AddDelivery(User user, Delivery delivery)
        {
            if (user == null || delivery == null) return;
            if (user.Deliveries.Contains(delivery)) return;
            user.Deliveries.Add(delivery);
        }

        public void EditDelivery(User user, Delivery delivery)
        {
            if (user == null || delivery == null) return;
            if (!user.Deliveries.Contains(delivery)) return;

            int index = user.Deliveries.IndexOf(delivery);
            user.Deliveries[index] = delivery;

            Edit(user, deliveries: user.Deliveries);
        }

        public void DeleteDelivery(User user, Delivery delivery)
        {
            if (user == null || delivery == null) return;
            if (!user.Deliveries.Contains(delivery)) return;
            user.Deliveries.Remove(delivery);
            Edit(user, deliveries: user.Deliveries);
        }
    }
}
