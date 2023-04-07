using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class DeliveryService : IDeliveryService
    {
        private static DeliveryList deliveryList = new DeliveryList();

        public List<Delivery> GetAllDeliveries(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;
            List<Delivery> lst = new List<Delivery>();

            foreach (Delivery delivery in DeliveryService.deliveryList.Deliveries)
            {
                if (delivery.UserId == userId) { lst.Add(delivery); }
            }

            if (lst.Count > 0) return lst;
            return null;
        }

        public List<Package> GetAllPackages(string id)
        {
            throw new NotImplementedException();
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

        public bool PackageExists(string deliveryId, string userId, string packageId)
        {
            throw new NotImplementedException();
        }

        public Delivery Get(string deliveryId, string userId)
        {
            if (!Exists(deliveryId, userId)) return null;
            return GetAllDeliveries(userId).Find(x => x.Id == deliveryId);
        }

        public int Price(string deliveryId, string userId)
        {
            throw new NotImplementedException();
        }

        public string Status(string deliveryId, string userId)
        {
            throw new NotImplementedException();
        }

        // cost and deliveryStatus might be needed to reavluate and changed.
        public void Edit(string deliveryId, string userId, List<Package>? packages = null, Container? container = null)
        {
            throw new NotImplementedException();
        }

        public void EditPackage(string deliveryId, string userId, string packageId, string? type, int? amount, int? width, int? height, int? depth, bool? isFragile, int? cost, string? adress)
        {
            throw new NotImplementedException();
        }

        public Package GetPackage(string deliveryId, string userId, string packageId)
        {
            throw new NotImplementedException();
        }

        public int GetPackageCount(string id)
        {
            throw new NotImplementedException();
        }

        public int GetPackageCount(string deliveryId, string userId)
        {
            throw new NotImplementedException();
        }

        public int AddPackage(string deliveryId, Package package)
        {
            throw new NotImplementedException();
        }

        public int DeletePackage(string deliveryId, string userId, string packageId)
        {
            throw new NotImplementedException();
        }
    }
}
