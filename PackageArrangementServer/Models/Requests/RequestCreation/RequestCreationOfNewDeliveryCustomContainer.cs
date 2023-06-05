namespace PackageArrangementServer.Models.Requests.RequestCreation
{
    public class RequestCreationOfNewDeliveryCustomContainer
    {
        public DateTime DeliveryDate { get; set; }
        public List<RequestCreationOfNewPackageInNewDelivery> Packages { get; set; }
        public GeneralContainer Container { get; set; }

    }
}