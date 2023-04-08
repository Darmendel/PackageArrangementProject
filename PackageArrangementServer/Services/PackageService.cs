using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class PackageService : IPackageService
    {
        private static PackageList packageList = new PackageList();

        public List<Package> GetAllPackages(string deliveryId)
        {
            if (string.IsNullOrEmpty(deliveryId)) return null;
            List<Package> lst = new List<Package>();

            foreach (Package package in PackageService.packageList.Packages)
            {
                if (package.DeliveryId == deliveryId) lst.Add(package);
            }
            
            if (lst.Count > 0) return lst;
            return null;
        }

        public bool Exists(string packageId, string deliveryId)
        {
            if (string.IsNullOrEmpty(packageId) || string.IsNullOrEmpty(deliveryId)) return false;

            List<Package> packages = GetAllPackages(deliveryId);
            if (packages == null) return false;

            foreach (Package package in packages)
            {
                if (package.Id == packageId) return true;
            }
            return false;
        }

        public int Count(string deliveryId)
        {
            if (string.IsNullOrEmpty(deliveryId)) return -1; // or 0

            List<Package> packages = GetAllPackages(deliveryId);
            if (packages == null) return 0; // (empty)

            return packages.Count;
        }

        public Package Get(string packageId, string deliveryId)
        {
            if (!Exists(packageId, deliveryId)) return null;
            return GetAllPackages(deliveryId).Find(x => x.Id == packageId);
        }

        public void Edit(string packageId, string deliveryId, string? type = null, string? amount = null, string? width = null,
            string? height = null, string? depth = null, string? cost = null, string? address = null)
        {
            Package package = Get(packageId, deliveryId);
            if (package == null) return;
            PackageService.packageList.Edit(package, type, amount, width, height, depth, cost, address);
        }

        public void Delete(string packageId, string deliveryId)
        {
            Package package = Get(packageId, deliveryId);
            if (package == null) return;
            PackageService.packageList.Remove(package);
        }
    }
}
