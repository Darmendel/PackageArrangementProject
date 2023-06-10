using PackageArrangementServer.Models.Containers;

namespace PackageArrangementServer.Models.Requests.RequestCreation
{
    public class RequestCreationDeliveryCustomContainer
    {
        public DateTime DeliveryDate { get; set; }
        public List<RequestCreationOfNewPackageInNewDelivery> Packages { get; set; }
        public NoCostContainer Container { get; set; }
    }
}
