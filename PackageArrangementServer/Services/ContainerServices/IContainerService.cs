using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IContainerService
    {
        /// <summary>
        /// Validates that the given size represents an actual container.
        /// </summary>
        /// <param name="size"></param>
        /// <returns>bool</returns>
        bool Validate(ContainerSize size);

        /// <summary>
        /// Validates that the given size represents an actual container.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="depth"></param>
        /// <returns>bool</returns>
        bool Validate(string height, string width, string depth);

        /// <summary>
        /// Returns the type of the container - small, medium or large.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        string Type(ContainerSize size);

        /// <summary>
        /// Returns the type of the container - small, medium or large.
        /// </summary>
        /// <param name="container"></param>
        /// <returns>string</returns>
        string Type(IContainer container);

        /// <summary>
        /// Returns a numerical value of the container's type.
        /// </summary>
        /// <param name="container"></param>
        /// <returns>int</returns>
        int Size(IContainer container);

        /// <summary>
        /// Returns a container by size.
        /// </summary>
        /// <param name="size"></param>
        /// <returns>IContainer</returns>
        IContainer Get(ContainerSize size);

        /// <summary>
        /// Creates a new container.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="depth"></param>
        /// <returns>IContainer</returns>
        public IContainer Create(string height, string width, string depth);
    }
}
