namespace PackageArrangementServer.Models.Requests.RequestCreation
{
    public class DeliveryRequest
    {
        public string Id { get; set; }
        public IContainer Container { get; set; }
        public List<Package> Packages { get; set; }
    }
}
