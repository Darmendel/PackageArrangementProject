namespace PackageArrangementServer.Models
{
    public class RequestCreationOfNewDelivery
    {
        public DateTime DeliveryDate { get; set; }
        public List<Package> Packages { get; set; }
        public Container? Container { get; set; }
    }
}
