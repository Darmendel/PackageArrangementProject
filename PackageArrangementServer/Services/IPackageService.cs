namespace PackageArrangementServer.Services
{
    public interface IPackageService
    {
        public void EditPackage(string packageId, string? type, int? amount, int? width, int? height, int? depth, bool? isFragile, int? cost, string? adress);
    }
}
