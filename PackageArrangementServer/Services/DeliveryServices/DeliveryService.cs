using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class DeliveryService : IDeliveryService
    {
        private IContainerService containerService;
        private IPackageService packageService;
        private IDeliveryServiceHelper helper;
        
        private static DeliveryList deliveryList;

        public DeliveryService(IContainerService cs, IPackageService ps, IDeliveryServiceHelper dsh)
        {
            this.containerService = cs;
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

            //if (lst.Count > 0) return lst;
            return lst; // Returns empty lists as well
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

        /*private Package ConvertToPackage(RequestCreationOfNewPackage request)
        {
            return packageService.ConvertToPackage(request);
        }*/

        private string CreateDeliveryId(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;

            var random = new Random();
            string deliveryId = random.Next(0, 999).ToString();

            while (Exists(deliveryId, userId)) deliveryId = random.Next(0, 999).ToString() + deliveryId;
            return deliveryId;
        }

        public Delivery Create(string userId, DateTime? deliveryDate = null, List<RequestCreationOfNewPackageInNewDelivery> packages = null,
            IContainer container = null)
        {
            string deliveryId = CreateDeliveryId(userId);
            if (deliveryId == null) return null;

            List<Package> packageList = packageService.GetPackageList(deliveryId, packages);
            if (packageList == null) packageList = new List<Package>();

            Delivery delivery = new Delivery(deliveryId, userId, deliveryDate, packageList, container);

            string cost = Cost(delivery).ToString();
            DeliveryStatus status = Status(delivery);

            delivery.Cost = cost;
            delivery.Status = status;

            DeliveryService.deliveryList.Add(delivery);
            return delivery;
        }

        public Delivery Update(string deliveryId, string userId, List<Package>? p)
        {
            if (!Exists(deliveryId, userId)) return null;
            return Edit(deliveryId, userId, packages: p);
        }

        public Delivery Update(string deliveryId, string userId, DateTime? d)
        {
            if (!Exists(deliveryId, userId)) return null;
            return Edit(deliveryId, userId, deliveryDate: d);
        }

        public Delivery Update(string deliveryId, string userId, IContainer c)
        {
            if (!Exists(deliveryId, userId)) return null;
            return Edit(deliveryId, userId, container: c);
        }

        /*private Package ConvertToPackage(string deliveryId, RequestEditPackage request)
        {
            return packageService.ConvertToPackage(deliveryId, request);
        }*/

        /*private List<Package> GetPackageList(string deliveryId, List<RequestEditPackage> packages)
        {
            if (packages == null) return null;
            List<Package> packageList = new List<Package>();

            foreach (RequestEditPackage package in packages)
            {
                Package p = ConvertToPackage(deliveryId, package);
                if (p != null) packageList.Add(p);
            }
            return packageList;
        }*/

        // cost and deliveryStatus might be needed to reavluate and changed.
        /*public Delivery Edit(string deliveryId, string userId, DateTime? deliveryDate = null,
            List<RequestEditPackage>? packages = null, IContainer container = null)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;

            List<Package> packageList = GetPackageList(deliveryId, packages);
            if (packageList == null) packageList = new List<Package>();

            string cost = Cost(deliveryId, userId).ToString();
            DeliveryStatus status = Status(deliveryId, userId);

            DeliveryService.deliveryList.Edit(delivery, deliveryDate, packageList, container, cost, status);
            return Get(deliveryId, userId);
        }*/

        public Delivery Edit(string deliveryId, string userId, DateTime? deliveryDate = null,
            List<Package>? packages = null, IContainer container = null)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;

            if (deliveryDate == null) deliveryDate = delivery.DeliveryDate;
            if (packages == null) packages = GetAllPackages(deliveryId, userId);
            if (container == null) container = delivery.Container;  

            string cost = Cost(deliveryId, userId).ToString();
            DeliveryStatus status = Status(deliveryId, userId);

            DeliveryService.deliveryList.Edit(delivery, deliveryDate, packages, container, cost, status);
            return Get(deliveryId, userId);
        }

        /*public List<Delivery> EditDeliveryList(List<Delivery> list, Delivery delivery)
        {
            int index = list.IndexOf(delivery);
            if (index == -1) return null;

            list[index].DeliveryDate = delivery.DeliveryDate;
            list[index].Packages = delivery.Packages;
            list[index].Container = delivery.Container;
            list[index].Cost = delivery.Cost;
            list[index].Status = delivery.Status;

            return list;
        }*/

        public Delivery Delete(string deliveryId, string userId)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;
            DeliveryService.deliveryList.Remove(delivery);
            return delivery;
        }

        public IContainer GetContainer(ContainerSize size)
        {
            return containerService.Get(size);
        }

        public IContainer GetContainer(string deliveryId, string userId)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;
            return delivery.Container;
        }

        public IContainer CreateContainer(string height, string width, string depth)
        {
            return containerService.Create(height, width, depth);
        }

        public List<Package> GetAllPackages(string deliveryId, string userId)
        {
            if (!Exists(deliveryId, userId)) return null;
            return packageService.GetAllPackages(deliveryId);
        }

        public List<Package> GetPackageList(string deliveryId, string userId,
            List<RequestCreationOfNewPackageInNewDelivery> packages)
        {
            if (!Exists(deliveryId, userId)) return null;
            return packageService.GetPackageList(deliveryId, packages);
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

        public Package CreatePackage(string deliveryId, string userId, string amount = null, string width = null,
            string height = null, string depth = null, string address = null)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;

            Package package = packageService.Create(deliveryId, amount, width, height, depth, address);
            deliveryList.AddPackage(delivery, package);
            return package;
        }

        public Package EditPackage(string deliveryId, string userId, string packageId, string amount = null,
            string width = null, string height = null, string depth = null, string address = null)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;

            Package package = packageService.Edit(packageId, deliveryId, amount, width, height, depth, address);

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
