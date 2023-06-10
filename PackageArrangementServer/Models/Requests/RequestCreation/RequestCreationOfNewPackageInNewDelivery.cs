namespace PackageArrangementServer.Models
{
    public class RequestCreationOfNewPackageInNewDelivery : IRequestCreation
    {
        public string Width { get; set; }
        public string Height { get; set; }
        public string Length { get; set; }
        public string Order { get; set; }
    }
}
