namespace PackageArrangementServer.Models
{
    public class RequestEditDelivery
    {
        public DateTime DeliveryDate { get; set; }
        //public List<Package> Packages { get; set; }
        public Container? SelectedContainer { get; set; }
        public int Cost { get; set; }
        public string DeliveryStatus { get; set; }
    }
}
