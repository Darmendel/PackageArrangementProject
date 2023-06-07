using System.ComponentModel.DataAnnotations;

namespace PackageArrangementServer.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; } // add last name
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Delivery> Deliveries { get; set; }


        public User(string id, string name, string email, string password, List<Delivery> deliveries)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Deliveries = deliveries;
        }
        

        public User(string id, RegisterRequest reg)
        {
            Id = id;
            Name = reg.Name;
            Email = reg.Email;
            Password = reg.Password;
            Deliveries = new List<Delivery>();
        }
    }
}
