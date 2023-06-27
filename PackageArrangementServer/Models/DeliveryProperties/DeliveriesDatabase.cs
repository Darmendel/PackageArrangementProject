namespace PackageArrangementServer.Models.DeliveryProperties
{
    public class DeliveriesDatabase
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string DeliveriesCollectionName { get; set; } = null!;
    }
}
