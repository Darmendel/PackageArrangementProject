using PackageArrangementServer.Models.Containers;

namespace PackageArrangementServer.Models
{
    public class RequestEditDelivery
    {
        public DateTime DeliveryDate { get; set; }
        public List<Package> Packages { get; set; }
        public IContainer? Container { get; set; }
        public string Cost { get; set; } // add type check
        public string DeliveryStatus { get; set; }
    }
}
