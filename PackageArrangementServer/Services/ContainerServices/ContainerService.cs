using PackageArrangementServer.Models;
using System.Text.RegularExpressions;

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

        public static bool Validate(string height, string width, string depth)
        {
            if (height == null || !Regex.IsMatch(height, @"^\d+$")) return false;
            if (width == null || !Regex.IsMatch(width, @"^\d+$")) return false;
            if (depth == null || !Regex.IsMatch(depth, @"^\d+$")) return false;

            return true;
        }

        public static string Type(ContainerSize size)
        {
            if (size == ContainerSize.Small) return "small";
            if (size == ContainerSize.Medium) return "medium";
            if (size == ContainerSize.Large) return "large";

            // Ignoring general
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

        public IContainer Create(string height, string width, string depth)
        {
            if (!Validate(height, width, depth)) return null;
            return new GeneralContainer(height, width, depth); // need to calculate it's cost!
        }

        bool IContainerService.Validate(string height, string width, string depth) =>
            ContainerService.Validate(height, width, depth);

        bool IContainerService.Validate(ContainerSize size) => ContainerService.Validate(size);

        string IContainerService.Type(ContainerSize size) => ContainerService.Type(size);

        string IContainerService.Type(IContainer container) => ContainerService.Type(container);

        int IContainerService.Size(IContainer container) => ContainerService.Size(container);

        IContainer IContainerService.Get(ContainerSize size) => ContainerService.Get(size);

    }
}
