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
            
            //if (lst.Count > 0) return lst;
            return lst; // Returns empty lists as well
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

        /*public Package ConvertToPackage(RequestCreationOfNewPackage request)
        {
            RequestCreationOfNewPackageInNewDelivery req = new RequestCreationOfNewPackageInNewDelivery()
            {
                Type = request.Type,
                Amount = request.Amount,
                Width = request.Width,
                Height = request.Height,
                Length = request.Length,
                Weight = request.Weight,
                Cost = request.Cost,
                Address = request.Address
            };
            return ConvertToPackage(request.DeliveryId, req);
        }*/

        public Package ConvertToPackage(string deliveryId, RequestCreationOfNewPackageInNewDelivery request)
        {
            if (deliveryId == null || request == null) return null;

            string packageId = CreatePackageId(deliveryId);
            if (packageId == null) return null;

            return new Package(packageId, deliveryId, request.Width, request.Height, request.Length, request.Order);
        }

        public Package ConvertToPackage(string deliveryId, RequestEditPackage request)
        {
            if (deliveryId == null || request == null) return null;

            string packageId = CreatePackageId(deliveryId);
            if (packageId == null) return null;

            return new Package(packageId, deliveryId, request.Width, request.Height, request.Length, request.Order);
        }

        public List<Package> GetPackageList(string deliveryId, List<RequestCreationOfNewPackageInNewDelivery> packages)
        {
            if (packages == null) return null;
            List<Package> packageList = new List<Package>();

            foreach (RequestCreationOfNewPackageInNewDelivery package in packages)
            {
                Package p = ConvertToPackage(deliveryId, package);
                if (p != null) packageList.Add(p);
            }
            return packageList;
        }

        public Package Create(string deliveryId, string width = null, string height = null,
            string Length = null, string order = null)
        {
            string packageId = CreatePackageId(deliveryId);
            if (packageId == null) return null;

            Package package = new Package(packageId, deliveryId, width, height, Length, order);

            PackageService.packageList.Add(package);
            return package;
        }

        public Package Edit(string packageId, string deliveryId, string width = null,
            string height = null, string Length = null)
        {
            Package package = Get(packageId, deliveryId);
            if (package == null) return null;

            PackageService.packageList.Edit(package, width, height, Length);
            return Get(packageId, deliveryId);
        }

        public List<Package> EditPackageList(List<Package> list, Package package)
        {
            int index = list.IndexOf(package);
            if (index == -1) return null;

            list[index].Width = package.Width;
            list[index].Height = package.Height;
            list[index].Length = package.Length;

            return list;
        }

        public Package Delete(string packageId, string deliveryId)
        {
            Package package = Get(packageId, deliveryId);
            if (package == null) return null;
            PackageService.packageList.Remove(package);
            return package;
        }
    }
}
