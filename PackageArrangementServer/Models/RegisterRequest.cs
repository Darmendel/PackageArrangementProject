namespace PackageArrangementServer.Models
{
    public class RegisterRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public RegisterRequest(string id, string name, string password)
        {
            this.Id = id;
            this.Name = name;
            this.Password = password;
        }
    }
}
