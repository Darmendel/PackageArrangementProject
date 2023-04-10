namespace PackageArrangementServer.Models
{
    public class RequestCreationOfNewDelivery
    {
        public string UserId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<Package> Packages { get; set; }
        public Container? Container { get; set; }
    }
}
