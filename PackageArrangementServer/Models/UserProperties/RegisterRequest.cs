using System.ComponentModel.DataAnnotations;

namespace PackageArrangementServer.Models
{
    public class RegisterRequest
    {
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
         public RegisterRequest(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }
    }
}
