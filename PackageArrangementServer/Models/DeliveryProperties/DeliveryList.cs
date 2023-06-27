﻿namespace PackageArrangementServer.Models
{
    public class DeliveryList
    {
        private List<Delivery> _deliveries;
        //private readonly List<Delivery> deliveries;

        public DeliveryList()
        {
            _deliveries = new List<Delivery>();
        }

        public DeliveryList(List<Delivery> deliveries)
        {
            _deliveries = deliveries;
        }

        public List<Delivery> Deliveries { get { return _deliveries; } } // need to fix - return a copy

        //public bool IsEmpty { get { return _deliveries.Count == 0; } }

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

        /*private bool ValidateContainer(ContainerSize size)
        {
            if (size == ContainerSize.Small
                || size == ContainerSize.Medium
                || size == ContainerSize.Large) return true;
            return false;
        }*/

        public void Edit(Delivery delivery, DateTime? deliveryDate = null, List<Package> packages = null,
            IContainer container = null, string cost = null, DeliveryStatus? status = null)
        {
            if (delivery == null) return;

            if (_deliveries.Contains(delivery))
            {
                int index = _deliveries.IndexOf(delivery);

                if (deliveryDate != null) _deliveries[index].DeliveryDate = (DateTime) deliveryDate;
                //if (packages != null) _deliveries[index].Packages = packages;
                if (container != null) _deliveries[index].Container = container;

                /*if (container != null)
                {
                    if (ValidateContainer(container.Size)) _deliveries[index].Container = container;
                }*/

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
            if (!delivery.FirstPackages.Contains(package)) return;

            int index = delivery.FirstPackages.IndexOf(package);
            delivery.FirstPackages[index] = package;

            Edit(delivery, packages: delivery.FirstPackages);
        }

        public void DeletePackage(Delivery delivery, Package package)
        {
            if (delivery == null || package == null) return;
            if (!delivery.FirstPackages.Contains(package)) return;
            delivery.FirstPackages.Remove(package);
            Edit(delivery, packages: delivery.FirstPackages);
        }
    }
}
