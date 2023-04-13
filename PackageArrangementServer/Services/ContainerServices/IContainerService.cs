using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IContainerService
    {
        public bool Validate(ContainerSize size);
    }
}
