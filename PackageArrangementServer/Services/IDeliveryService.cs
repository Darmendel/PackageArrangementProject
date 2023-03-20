using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IDeliveryService
    {
        public List<Package> GetAllPackages(string id);
        public bool Exists(string id);
        public bool Exists(string id, string userId); // maybe drop the userId

        //public int CreateDelivery();
        public void Edit(string id, string userId, List<Package>? packages, Container? container, int? cost, string? deliveryStatus); // maybe drop the userId
        public bool PackageExists(string id, string userId, string packageId); // maybe drop the userId
        public Package GetPackage(string id, string userId, string packageId); // maybe drop the userId
        public int GetPackageCount(string id);
        public int GetPackageCount(string id, string userId); // maybe drop the userId
        public int AddPackage(string id);
        public void EditPackage(string id, string userId, string packageId, string? type, int? amount, int? width, int? height, int? depth, bool? isFragile, int? cost, string? adress); // maybe drop the userId
        public int DeletePackage(string id, string userId, string packageId); // maybe drop the userId
    }
}
