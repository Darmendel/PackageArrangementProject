using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IDeliveryService
    {
        public List<Package> GetAllPackages(string id);
        public bool Exists(string id);
        public bool Exists(string id, string userId); // maybe drop the userId
        public bool PackageExists(string id, string userId, string packageId); // maybe drop the userId

        //public int CreateDelivery();
        public void Edit(string id, string userId, List<Package>? packages = null, Container? container = null, int? cost = null, string? deliveryStatus = null); // maybe drop the userId
        public void EditPackage(string id, string userId, string packageId, string? type = null, int? amount = null, int? width = null, int? height = null, int? depth = null, bool? isFragile = null, int? cost = null, string? adress = null); // maybe drop the userId
        public Package GetPackage(string id, string userId, string packageId); // maybe drop the userId
        public int GetPackageCount(string id);
        public int GetPackageCount(string id, string userId); // maybe drop the userId
        public int AddPackage(string id);
        public int DeletePackage(string id, string userId, string packageId); // maybe drop the userId
    }
}
