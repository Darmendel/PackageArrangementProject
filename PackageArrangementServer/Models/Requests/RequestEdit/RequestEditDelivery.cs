namespace PackageArrangementServer.Models
{
    public class RequestEditDelivery
    {
        public DateTime DeliveryDate { get; set; }
        public List<RequestEditPackage> Packages { get; set; }
        public IContainer? Container { get; set; }
        public string Cost { get; set; } // add type check
        public string DeliveryStatus { get; set; }
    }
}
