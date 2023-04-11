using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class PackageService : IPackageService
    {
        private static PackageList packageList;

        public PackageService()
        {
            //packageList = new PackageList();
            packageList = StaticData.GetPackages();
        }

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

        private string CreatePackageId(string deliveryId)
        {
            if (string.IsNullOrEmpty(deliveryId)) return null;

            var random = new Random();
            string packageId = random.Next(0, 999).ToString();

            while (Exists(packageId, deliveryId)) packageId = random.Next(0, 999).ToString() + packageId;
            return packageId;
        }

        public Package ConvertToPackage(RequestCreationOfNewPackage request)
        {
            if (request == null) return null;

            string packageId = CreatePackageId(request.DeliveryId);
            if (packageId == null) return null;

            return new Package(packageId, request.Type, request.DeliveryId, request.Amount, request.Width,
                request.Height, request.Depth, request.Weight, request.Cost, request.Address);
        }

        public int Add(string deliveryId, string type = null, string amount = null, string width = null, string height = null,
            string depth = null, string weight = null, string cost = null, string address = null)
        {
            string packageId = CreatePackageId(deliveryId);
            if (packageId == null) return 0;

            PackageService.packageList.Add(new Package(packageId, deliveryId, type, amount, width, height,
                depth, weight, cost, address));
            return 1;
        }

        public int Edit(string packageId, string deliveryId, string type = null, string amount = null, string width = null,
            string height = null, string depth = null, string weight = null, string cost = null, string address = null)
        {
            Package package = Get(packageId, deliveryId);
            if (package == null) return 0;
            PackageService.packageList.Edit(package, type, amount, width, height, depth, weight, cost, address);
            return 1;
        }

        public int Delete(string packageId, string deliveryId)
        {
            Package package = Get(packageId, deliveryId);
            if (package == null) return 0;
            PackageService.packageList.Remove(package);
            return 1;
        }
    }
}
