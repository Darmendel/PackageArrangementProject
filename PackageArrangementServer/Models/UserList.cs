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

        public List<User> Users { get { return _users; } } // need to fix - return a copy

        public int Count { get { return _users.Count; } }

        public void Add(User user)
        {
            if (user == null || _users.Contains(user)) return;
            _users.Add(user);
        }

        public void Edit(User user, string? name = null, string? password = null)
        {
            if (user == null) return;
            if (_users.Contains(user))
            {
                int index = _users.IndexOf(user);

                if (name != null) _users[index].Name = name;
                if (password != null) _users[index].Password = password;
            }
        }
    }
}
