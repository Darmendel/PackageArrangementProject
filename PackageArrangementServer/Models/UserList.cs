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
    }
}
