using System.Text.Json.Serialization;

namespace PackageArrangementServer.Models.Requests.RequestCreation
{
    public class DeliveryRequest
    {
        
        public string Id { get; set; }
        public IContainer Container { get; set; }
        public List<Package> Packages { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string UserId { get; set; }


        public DeliveryRequest(string id, IContainer container, List<Package> packages, string userId = null) { 
            Id = id;
            Container = container;
            Packages = packages;
            UserId = userId;
        }
    }
}
