namespace PackageArrangementServer.Models
{
    public class RequestCreationOfNewPackage : IRequestCreation
    {
        public string DeliveryId { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Length { get; set; }
        public string Order { get; set; }
    }
}
