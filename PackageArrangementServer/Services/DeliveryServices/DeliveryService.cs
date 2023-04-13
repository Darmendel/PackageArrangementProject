using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class DeliveryService : IDeliveryService
    {
        private IPackageService packageService;
        private IDeliveryServiceHelper helper;
        private static DeliveryList deliveryList;

        public DeliveryService(IPackageService ps, IDeliveryServiceHelper dsh)
        {
            this.packageService = ps;
            this.helper = dsh;
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

            //return lst;
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
            return helper.Cost(delivery);
        }

        public int Cost(string deliveryId, string userId)
        {
            //Delivery delivery = Get(deliveryId, userId);
            //return Cost(delivery);

            return helper.Cost(Get(deliveryId, userId));
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

        /*private Delivery Update(Delivery delivery)
        {
            if (delivery == null) return null;
            DeliveryService.deliveryList.Edit(delivery, cost: Cost(delivery).ToString(), status: Status(delivery));
            return Get(delivery.Id, delivery.UserId);
        }

        private Delivery Add(Delivery delivery)
        {
            string cost = Cost(delivery).ToString();
            DeliveryStatus status = Status(delivery);

            delivery.Cost = cost;
            delivery.Status = status;

            DeliveryService.deliveryList.Add(delivery);
            return Update(delivery);
        }*/

        private Package ConvertToPackage(RequestCreationOfNewPackage request)
        {
            return packageService.ConvertToPackage(request);
        }

        private List<Package> GetPackageList(List<RequestCreationOfNewPackage> packages)
        {
            if (packages == null) return null;
            List<Package> packageList = new List<Package>();

            foreach (RequestCreationOfNewPackage package in packages)
            {
                Package p = ConvertToPackage(package);
                if (p != null) packageList.Add(p);
            }
            return packageList;
        }

        private string CreateDeliveryId(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;

            var random = new Random();
            string deliveryId = random.Next(0, 999).ToString();

            while (Exists(deliveryId, userId)) deliveryId = random.Next(0, 999).ToString() + deliveryId;
            return deliveryId;
        }

        public Delivery Create(string userId, DateTime? deliveryDate = null, List<RequestCreationOfNewPackage> packages = null,
            IContainer container = null)
        {
            string deliveryId = CreateDeliveryId(userId);
            if (deliveryId == null) return null;

            List<Package> packageList = GetPackageList(packages);
            if (packageList == null) packageList = new List<Package>();

            Delivery delivery = new Delivery(deliveryId, userId, deliveryDate, packageList, container);

            string cost = Cost(delivery).ToString();
            DeliveryStatus status = Status(delivery);

            delivery.Cost = cost;
            delivery.Status = status;

            DeliveryService.deliveryList.Add(delivery);
            return delivery;
        }

        // cost and deliveryStatus might be needed to reavluate and changed.
        public Delivery Edit(string deliveryId, string userId, DateTime? deliveryDate = null,
            List<Package>? packages = null, IContainer container = null)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;

            string cost = Cost(deliveryId, userId).ToString();
            DeliveryStatus status = Status(deliveryId, userId);

            DeliveryService.deliveryList.Edit(delivery, deliveryDate, packages, container, cost, status);
            return Get(deliveryId, userId);
        }

        public List<Delivery> EditDeliveryList(List<Delivery> list, Delivery delivery)
        {
            int index = list.IndexOf(delivery);
            if (index == -1) return null;

            list[index].DeliveryDate = delivery.DeliveryDate;
            list[index].Packages = delivery.Packages;
            list[index].Container = delivery.Container;
            list[index].Cost = delivery.Cost;
            list[index].Status = delivery.Status;

            return list;
        }

        public Delivery Delete(string deliveryId, string userId)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;
            DeliveryService.deliveryList.Remove(delivery);
            return delivery;
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

        public Package CreatePackage(string deliveryId, string userId, string type = null, string amount = null, string width = null,
            string height = null, string depth = null, string weight = null, string cost = null, string address = null)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;

            Package package = packageService.Create(deliveryId, type, amount, width, height, depth, weight, cost, address);
            deliveryList.AddPackage(delivery, package);
            return package;
        }

        public Package EditPackage(string deliveryId, string userId, string packageId, string type = null,
            string amount = null, string width = null, string height = null, string depth = null, string weight = null,
            string cost = null, string address = null)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;

            Package package = packageService.Edit(packageId, deliveryId, type, amount, width, height, depth,
                weight, cost, address);

            deliveryList.EditPackage(delivery, package);
            return package;
        }

        public List<Package> EditPackageList(string deliveryId, string userId, List<Package> list, Package package)
        {
            if (!Exists(deliveryId, userId) || list == null || package == null) return null;
            return packageService.EditPackageList(list, package);
        }

        public Package DeletePackage(string deliveryId, string userId, string packageId)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;

            Package package = packageService.Delete(packageId, deliveryId);
            deliveryList.DeletePackage(delivery, package);
            return package;
        }
    }
}
