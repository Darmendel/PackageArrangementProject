using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class DeliveryService : IDeliveryService
    {
        public int AddPackage(string id)
        {
            throw new NotImplementedException();
        }

        public int DeletePackage(string id, string userId, string packageId)
        {
            throw new NotImplementedException();
        }

        public void Edit(string id, string userId, List<Package>? packages, Container? container, int? cost, string? deliveryStatus)
        {
            throw new NotImplementedException();
        }

        public void EditPackage(string id, string userId, string packageId, string? type, int? amount, int? width, int? height, int? depth, bool? isFragile, int? cost, string? adress)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string id, string userId)
        {
            throw new NotImplementedException();
        }

        public Package GetPackage(string id, string userId, string packageId)
        {
            throw new NotImplementedException();
        }

        public int GetPackageCount(string id)
        {
            throw new NotImplementedException();
        }

        public int GetPackageCount(string id, string userId)
        {
            throw new NotImplementedException();
        }

        public List<Package> GetPackages()
        {
            throw new NotImplementedException();
        }

        public bool PackageExists(string id, string userId, string packageId)
        {
            throw new NotImplementedException();
        }
    }
}
