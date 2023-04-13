using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public class ContainerService : IContainerService
    {
        public static bool Validate(ContainerSize size)
        {
            if (size == ContainerSize.Small
                || size == ContainerSize.Medium
                || size == ContainerSize.Large) return true;
            return false;
        }

        public static string Type(ContainerSize size)
        {
            if (size == ContainerSize.Small) return "small";
            if (size == ContainerSize.Medium) return "medium";
            if (size == ContainerSize.Large) return "large";
            return null;
        }

        public static string Type(IContainer container)
        {
            return Type(container.Size);
        }

        public static int Size(IContainer container)
        {
            if (container == null || !Validate(container.Size)) return 0;
            return (int) container.Size;
        }

        public static IContainer Get(ContainerSize size)
        {
            string type = Type(size);

            if (type == "small") return new SmallContainer();
            if (type == "medium") return new MediumContainer();
            if (type == "large") return new BigContainer();

            return null;
        }

        bool IContainerService.Validate(ContainerSize size) => ContainerService.Validate(size);

        string IContainerService.Type(ContainerSize size) => ContainerService.Type(size);

        string IContainerService.Type(IContainer container) => ContainerService.Type(container);

        int IContainerService.Size(IContainer container) => ContainerService.Size(container);

        IContainer IContainerService.Get(ContainerSize size) => ContainerService.Get(size);

    }
}
