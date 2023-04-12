namespace PackageArrangementServer.Models
{
    public class RequestCreationOfNewDelivery
    {
        public DateTime DeliveryDate { get; set; }
        public List<RequestCreationOfNewPackage> Packages { get; set; }
        //public IContainer? Container { get; set; }
    }
}
