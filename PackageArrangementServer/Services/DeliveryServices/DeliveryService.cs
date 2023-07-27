using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PackageArrangementServer.Models;
using PackageArrangementServer.Models.DeliveryProperties;

namespace PackageArrangementServer.Services
{
    public class DeliveryService : IDeliveryService
    {
        private IPackageService packageService;
        private IDeliveryServiceHelper helper;
        
        private static DeliveryList deliveryList = new DeliveryList();

        public DeliveryService(IPackageService ps, IDeliveryServiceHelper dsh)
        {
            this.packageService = ps;
            this.helper = dsh;
        }

        public static List<Delivery> GetAllDeliveries(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;
            List<Delivery> lst = new List<Delivery>();

            foreach (Delivery delivery in DeliveryService.deliveryList.Deliveries)
            {
                if (delivery.UserId == userId) lst.Add(delivery);
            }

            return lst;
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

        public bool Exists(string deliveryId)
        {
            foreach (Delivery delivery in DeliveryService.deliveryList.Deliveries)
                if (delivery.Id == deliveryId) return true;
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

        private string CreateDeliveryId(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;

            var random = new Random();
            string deliveryId = ObjectId.GenerateNewId().ToString();

            while (Exists(deliveryId)) deliveryId = ObjectId.GenerateNewId().ToString();
            return deliveryId;
        }

        public Delivery Create(string userId, DateTime? deliveryDate = null, List<RequestCreationOfNewPackageInNewDelivery> packages = null,
            IContainer container = null)
        {
            string deliveryId = CreateDeliveryId(userId);
            if (deliveryId == null) return null;

            List<Package> packageList = packageService.GetPackageList(deliveryId, packages);
            if (packageList == null) packageList = new List<Package>();

            Delivery delivery = new Delivery(deliveryId, userId, deliveryDate, packageList, packageList, container);

            string cost = Cost(delivery).ToString();
            DeliveryStatus status = Status(delivery);

            delivery.Cost = cost;
            delivery.Status = status;

            DeliveryService.deliveryList.Add(delivery);
            return delivery;
        }
        public void Update(string deliveryId, Delivery delivery)
        {
            ;
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

        public Delivery Delete(string deliveryId, string userId)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;
            DeliveryService.deliveryList.Remove(delivery);
            return delivery;
        }

        public IContainer GetContainer(string deliveryId, string userId)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;
            return delivery.Container;
        }

        public List<Package> GetAllPackages(string deliveryId, string userId)
        {
            if (!Exists(deliveryId, userId)) return null;
            Delivery delivery = Get(deliveryId, userId);
            List<Package> lst = new List<Package>();

            foreach (Package package in delivery.FirstPackages)
            {
                lst.Add(package.Clone());
            }
            return lst;
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

        public Package CreatePackage(string deliveryId, string userId, string width = null,
            string height = null, string Length = null)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;

            Package package = packageService.Create(deliveryId, width, height, Length);
            deliveryList.AddPackage(delivery, package);
            return package;
        }

        public Package EditPackage(string deliveryId, string userId, string packageId, 
                        string width = null, string height = null, string Length = null)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return null;

            Package package = packageService.Edit(packageId, deliveryId, width, height, Length);

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
