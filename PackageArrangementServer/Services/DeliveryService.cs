using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class DeliveryService : IDeliveryService
    {
        private IPackageService packageService;
        private static DeliveryList deliveryList = new DeliveryList();
        //private static List<DeliveryList> deliveryList = new List<DeliveryList>();

        public DeliveryService(IPackageService ps)
        {
            this.packageService = ps;
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

        public int Cost(string deliveryId, string userId)
        {
            throw new NotImplementedException();
        }

        public string Status(string deliveryId, string userId)
        {
            throw new NotImplementedException();
        }

        // cost and deliveryStatus might be needed to reavluate and changed.
        public void Edit(string deliveryId, string userId, DateTime? deliveryDate = null, List<Package>? packages = null, Container? container = null)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return;

            string cost = Cost(deliveryId, userId).ToString();
            string status = Status(deliveryId, userId);

            DeliveryService.deliveryList.Edit(delivery, deliveryDate, packages, container, cost, status);
        }

        public void Delete(string deliveryId, string userId)
        {
            Delivery delivery = Get(deliveryId, userId);
            if (delivery == null) return;
            DeliveryService.deliveryList.Remove(delivery);
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

        public int AddPackage(string deliveryId, Package package)
        {
            throw new NotImplementedException();
        }

        public void EditPackage(string deliveryId, string userId, string packageId, string? type, int? amount, int? width, int? height, int? depth, bool? isFragile, int? cost, string? adress)
        {
            throw new NotImplementedException();
        }

        public int DeletePackage(string deliveryId, string userId, string packageId)
        {
            throw new NotImplementedException();
        }
    }
}
