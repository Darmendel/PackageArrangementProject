using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class PackageService : IPackageService
    {
        public Package Get(string id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string id)
        {
            throw new NotImplementedException();
        }

        public void Edit(string id, string? type = null, int? amount = null, int? width = null,
            int? height = null, int? depth = null, bool? isFragile = null, int? cost = null, string? address = null)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
