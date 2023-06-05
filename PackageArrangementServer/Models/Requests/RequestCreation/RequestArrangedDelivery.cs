namespace PackageArrangementServer.Models.Requests.RequestCreation
{
    public class RequestArrangedDelivery : RequestCreationOfNewPackageInNewDelivery
    {
        public string X { get; set; }
        public string Y { get; set; }
        public string Z { get; set; }
    }
}
