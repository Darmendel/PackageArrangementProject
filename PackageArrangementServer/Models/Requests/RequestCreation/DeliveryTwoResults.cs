using System.Text.Json.Serialization;

namespace PackageArrangementServer.Models.Requests.RequestCreation
{
    public class DeliveryTwoResults
    {
        public string Id { get; set; }
        //[JsonIgnore(Condition = JsonIgnoreCondition.Always)]

        public string UserId { get; set; }

        public IContainer Container { get; set; }
        public List<Package> FirstPackages { get; set; }
        public List<Package> SecondPackages { get; set; }


        public DeliveryTwoResults() { }
        public DeliveryTwoResults(string id, string userId, IContainer container, List<Package> fPackages, List<Package> sPackages)
        {
            Id = id;
            UserId = userId;
            Container = container;
            FirstPackages = fPackages;
            SecondPackages = sPackages;
        }

        public DeliveryTwoResults(DeliveryRequest req)
        {
            Id = req.Id;
            UserId = req.UserId;
            Container = req.Container;
            FirstPackages = req.Packages.Select(p => new Package
                (p.Id, p.DeliveryId, p.Width, p.Height, p.Length, p.Order)).ToList();
            SecondPackages = req.Packages.Select(p => new Package
                (p.Id, p.DeliveryId, p.Width, p.Height, p.Length, p.Order)).ToList();
        }
    }
}
