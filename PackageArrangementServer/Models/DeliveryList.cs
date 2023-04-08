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

        //public bool IsEmpty { get { return _deliveries.Count == 0; } }

        public void Add(Delivery delivery)
        {
            if (delivery == null || _deliveries.Contains(delivery)) return;
            _deliveries.Add(delivery);
        }

        public void Edit(Delivery delivery, DateTime? deliveryDate = null, List<Package> packages = null,
            Container container = null, string cost = null, string deliveryStatus = null)
        {
            if (delivery == null) return;

            if (_deliveries.Contains(delivery))
            {
                int index = _deliveries.IndexOf(delivery);

                if (deliveryDate != null) _deliveries[index].DeliveryDate = (DateTime) deliveryDate;
                if (packages != null) _deliveries[index].Packages = packages;
                if (container != null) _deliveries[index].SelectedContainer = container;
                if (cost != null) _deliveries[index].Cost = cost;
                if (deliveryStatus != null) _deliveries[index].DeliveryStatus = deliveryStatus;
            }
        }

        public void Remove(Delivery delivery)
        {
            if (deliveries == null) return;
            if (_deliveries.Contains(delivery)) _deliveries.Remove(delivery);
        }
    }
}
