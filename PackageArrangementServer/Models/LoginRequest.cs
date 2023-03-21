namespace PackageArrangementServer.Models
{
    public class LoginRequest
    {
        public string Id { get; set; }
        public string Password { get; set; }

        public LoginRequest(string id, string password)
        {
            this.Id = id;
            this.Password = password;
        }
    }
}
