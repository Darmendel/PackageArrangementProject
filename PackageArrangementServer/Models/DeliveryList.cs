namespace PackageArrangementServer.Models
{
    public class DeliveryList
    {
        private List<Delivery> _deliveries;
        private readonly List<Delivery> deliveries;

        public DeliveryList()
        {

        }

        public List<Delivery> Deliveries { get { return deliveries; } }

        public void Add(Delivery delivery)
        {
            if (delivery == null || _deliveries.Contains(delivery)) return;
            _deliveries.Add(delivery);
        }

        public void Edit() { } // alter

        public void Delete() { } // alter
    }
}
