using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PackageArrangementServer.Models.Requests.RequestCreation
{
    public class DeliveryResult
    {
        public string Id { get; set; }
        public GeneralContainer Container { get; set; }
        public List<Package> Packages { get; set; }

        public DeliveryResult(string id, GeneralContainer container, List<Package> packages)
        {
            Id = id;
            Container = container;
            Packages = packages;
        }
    }
}
