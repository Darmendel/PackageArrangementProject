namespace PackageArrangementServer.Models
{
    public class DeliveryList
    {
        private List<Delivery> _deliveries;

        public DeliveryList()
        {
            _deliveries = new List<Delivery>();
        }

        public DeliveryList(List<Delivery> deliveries)
        {
            _deliveries = deliveries;
        }

        public List<Delivery> Deliveries { get { return _deliveries; } }

        public void Add(Delivery delivery)
        {
            if (delivery == null || _deliveries.Contains(delivery)) return;
            _deliveries.Add(delivery);
        }

        public void Extend(DeliveryList deliveries)
        {
            foreach (Delivery delivery in deliveries.Deliveries)
            {
                _deliveries.Add(delivery);
            }
        }

        public void Edit(Delivery delivery, DateTime? deliveryDate = null, List<Package> packages = null,
            IContainer container = null, string cost = null, DeliveryStatus? status = null)
        {
            if (delivery == null) return;

            if (_deliveries.Contains(delivery))
            {
                int index = _deliveries.IndexOf(delivery);

                if (deliveryDate != null) _deliveries[index].DeliveryDate = (DateTime) deliveryDate;
                if (container != null) _deliveries[index].Container = container;
                if (cost != null) _deliveries[index].Cost = cost;
                if (status != null) _deliveries[index].Status = (DeliveryStatus) status;
            }
        }

        public void Remove(Delivery delivery)
        {
            if (_deliveries == null) return;
            if (_deliveries.Contains(delivery)) _deliveries.Remove(delivery);
        }

        public void AddPackage(Delivery delivery, Package package)
        {
            if (delivery == null || package == null) return;
            if (delivery.FirstPackages.Contains(package)) return;
            delivery.FirstPackages.Add(package);
            Edit(delivery, packages: delivery.FirstPackages);
        }

        public void EditPackage(Delivery delivery, Package package)
        {
            if (delivery == null || package == null) return;
            int index = containsId(delivery, package.Id);
            if (index == -1) return;

            delivery.FirstPackages[index] = package;

            Edit(delivery, packages: delivery.FirstPackages);
        }

        public void DeletePackage(Delivery delivery, Package package)
        {
            if (delivery == null || package == null) return;
            int index = containsId(delivery, package.Id);
            if (index == -1) return;
            delivery.FirstPackages.RemoveAt(index);
            Edit(delivery, packages: delivery.FirstPackages);
        }

        public int containsId(Delivery delivery, string id)
        {
            for (int i = 0; i < delivery.FirstPackages.Count; i++)
            {
                if (delivery.FirstPackages[i].Id == id) return i;
            }
            return -1;
        }
    }
}
