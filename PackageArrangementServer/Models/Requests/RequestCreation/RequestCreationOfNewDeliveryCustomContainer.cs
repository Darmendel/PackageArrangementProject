namespace PackageArrangementServer.Models.Requests.RequestCreation
{
    public class RequestCreationOfNewDeliveryCustomContainer
    {
        public DateTime DeliveryDate { get; set; }
        public List<RequestCreationOfNewPackageInNewDelivery> Packages { get; set; }
        public GeneralContainer Container { get; set; }


        public RequestCreationOfNewDeliveryCustomContainer(RequestCreationDeliveryCustomContainer con) {
            DeliveryDate = con.DeliveryDate;
            Packages = con.Packages;
            Container = new GeneralContainer(
                con.Container.Height, con.Container.Width, con.Container.Length);
        }

        public RequestCreationOfNewDeliveryCustomContainer(DateTime dateTime, List<RequestCreationOfNewPackageInNewDelivery> packages, GeneralContainer container) { 
            DeliveryDate = dateTime;
            Packages = packages;
            Container = container;
        }
    }
}