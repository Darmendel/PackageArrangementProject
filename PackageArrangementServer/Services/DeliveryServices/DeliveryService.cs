using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class DeliveryService : IDeliveryService
    {
        private IPackageService packageService;
        private static DeliveryList deliveryList;

        public DeliveryService(IPackageService ps)
        {
            this.packageService = ps;
            //deliveryList = new DeliveryList();
            deliveryList = StaticData.GetDeliveries();
        }

        public List<Delivery> GetAllDeliveries(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;
            List<Delivery> lst = new List<Delivery>();

            foreach (Delivery delivery in DeliveryService.deliveryList.Deliveries)
            {
                if (delivery.UserId == userId) lst.Add(delivery);
            }

            if (lst.Count > 0) return lst;
            return null;
        }

        public bool Exists(string deliveryId, string userId)
        {
            if (string.IsNullOrEmpty (deliveryId) || string.IsNullOrEmpty(userId)) return false;

            List<Delivery> deliveries = GetAllDeliveries(userId);
            if (deliveries == null) return false;

            foreach (Delivery delivery in deliveries)
            {
                if (delivery.Id == deliveryId) return true;
            }
            return false;
        }

        public Delivery Get(string deliveryId, string userId)
        {
            if (!Exists(deliveryId, userId)) return null;
            return GetAllDeliveries(userId).Find(x => x.Id == deliveryId);
        }

        public int Cost(Delivery delivery)
        {
            if (delivery == null) return -1;

            if (delivery.Packages == null && delivery.Container == null) return -1;

            int cost = 0;

            if (delivery.Packages != null)
            {
                foreach (Package package in delivery.Packages)
                {
                    if (package.Cost == null) continue;

                    try
                    {
                        int pc = Int32.Parse(package.Cost);
                        cost += pc;
                    }
                    catch (FormatException) { }
                }
            }

            if (delivery.Container != null)
            {
                try
                {
                    int c = Int32.Parse(delivery.Container.Cost);
                    cost += c;
                }
                catch (FormatException) { }
            }

            return cost;
        }

        public int Cost(string deliveryId, string userId)
        {
            Delivery delivery = Get(deliveryId, userId);
            return Cost(delivery);
        }

        public DeliveryStatus Status(Delivery delivery)
        {
            if (delivery == null) return DeliveryStatus.NonExisting;
            return delivery.Status;
        }

        public DeliveryStatus Status(string deliveryId, string userId)
        {
            Delivery delivery = Get(deliveryId, userId);
            return Status(delivery);
        }

        private void Update(string deliveryId, string userId)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return;
            DeliveryService.deliveryList.Edit(delivery, cost: Cost(delivery).ToString(), status: Status(delivery));
        }

        public int Add(string userId, DateTime? deliveryDate = null, List<Package> packages = null,
            Container container = null)
        {
            if (string.IsNullOrEmpty(userId)) return 0;

            var random = new Random();
            string id = random.Next(0, 999).ToString();

            while (Exists(id, userId)) id = random.Next(0, 999).ToString() + id;

            DeliveryService.deliveryList.Add(new Delivery(id, userId, deliveryDate, packages, container));
            Update(userId, userId);
            return 1;
        }

        // cost and deliveryStatus might be needed to reavluate and changed.
        public int Edit(string deliveryId, string userId, DateTime? deliveryDate = null, List<Package>? packages = null, Container? container = null)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return 0;

            string cost = Cost(deliveryId, userId).ToString();
            DeliveryStatus status = Status(deliveryId, userId);

            DeliveryService.deliveryList.Edit(delivery, deliveryDate, packages, container, cost, status);
            return 1;
        }

        public int Delete(string deliveryId, string userId)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return 0;
            DeliveryService.deliveryList.Remove(delivery);
            return 1;
        }

        public List<Package> GetAllPackages(string deliveryId, string userId)
        {
            if (!Exists(deliveryId, userId)) return null;
            return packageService.GetAllPackages(deliveryId);
        }

        public bool PackageExists(string deliveryId, string userId, string packageId)
        {
            if (!Exists(deliveryId, userId)) return false;
            return packageService.Exists(packageId, deliveryId);
        }

        public Package GetPackage(string deliveryId, string userId, string packageId)
        {
            if (!Exists(deliveryId, userId)) return null;
            return packageService.Get(packageId, deliveryId);
        }

        public int GetPackageCount(string deliveryId, string userId)
        {
            if (!Exists(deliveryId, userId)) return 0;
            return packageService.Count(deliveryId);
        }

        public int AddPackage(string deliveryId, string userId, string type = null, string amount = null, string width = null,
            string height = null, string depth = null, string weight = null, string cost = null, string address = null)
        {
            if (!Exists(deliveryId, userId)) return 0;
            return packageService.Add(deliveryId, type, amount, width, height, depth, weight, cost, address);
        }

        public int EditPackage(string deliveryId, string userId, string packageId, string type = null,
            string amount = null, string width = null, string height = null, string depth = null, string weight = null,
            string cost = null, string address = null)
        {
            if (!Exists(deliveryId, userId)) return 0;
            return packageService.Edit(deliveryId, packageId, type, amount, width, height, depth, weight, cost, address);
        }

        public int DeletePackage(string deliveryId, string userId, string packageId)
        {
            if (!Exists(deliveryId, userId)) return 0;
            return packageService.Delete(packageId, deliveryId);
        }
    }
}
