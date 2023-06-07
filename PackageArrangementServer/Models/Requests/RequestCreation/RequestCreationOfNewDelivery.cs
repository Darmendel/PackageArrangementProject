namespace PackageArrangementServer.Models
{
    public class RequestCreationOfNewDelivery : IRequestCreation
    {
        public DateTime DeliveryDate { get; set; }
        public List<RequestCreationOfNewPackageInNewDelivery> Packages { get; set; }
        public ContainerSize containerSize { get; set; }
        //public IContainer? Container { get; set; }
    }
}
