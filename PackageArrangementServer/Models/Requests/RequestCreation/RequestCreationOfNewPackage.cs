namespace PackageArrangementServer.Models
{
    public class RequestCreationOfNewPackage : IRequestCreation
    {
        public string DeliveryId { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Depth { get; set; }
        public string Weight { get; set; }
        //public bool IsFragile { get; set; }
        public string Cost { get; set; }
        public string Address { get; set; }
    }
}
