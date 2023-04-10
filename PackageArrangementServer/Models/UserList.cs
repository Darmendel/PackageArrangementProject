namespace PackageArrangementServer.Models
{
    public class UserList
    {
        private List<User> _users;

        public UserList(List<User> users)
        {
            foreach (User user in users)
            {
                users.Add(user);
            }
        }

        public UserList()
        {
            _users = new List<User>();
        }

        public List<User> Users { get { return _users; } } // need to fix - return a copy

        public int Count { get { return _users.Count; } }

        public void Add(User user)
        {
            if (user == null || _users.Contains(user)) return;
            _users.Add(user);
        }

        public void Edit(User user, string name = null, string password = null, List<Delivery> deliveries = null)
        {
            if (user == null) return;
            if (_users.Contains(user))
            {
                int index = _users.IndexOf(user);

                if (name != null) _users[index].Name = name;
                if (password != null) _users[index].Password = password;
                if (deliveries != null) _users[index].Deliveries = deliveries;
            }
        }

        private List<Delivery> GetDeliveryList(string id)
        {
            List<Delivery> lst = new List<Delivery>();
            //DateTime now = DateTime.Now;
            DateTime tomorrow = DateTime.Now.AddDays(1);

            for (int i = 0; i < 4; i++)
            {
                string ind = "" + i;
                if (id != ind) lst.Add(new Delivery(id, ind, tomorrow, new PackageList().Packages));
            }

            return lst;
        }

        public void Remove(User user)
        {
            if (_users.Contains(user)) _users.Remove(user);
        }
    }
}
