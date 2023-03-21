using PackageArrangementServer.Models;

namespace PackageArrangementServer.Services
{
    public interface IPackageService
    {
        /// <summary>
        /// Returns a package by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Package</returns>
        public Package Get(string id);

        /// <summary>
        /// Checks if a package exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public bool Exists(string id);

        /// <summary>
        /// Updates a package.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        /// <param name="isFragile"></param>
        /// <param name="cost"></param>
        /// <param name="address"></param>
        public void Edit(string id, string? type = null, int? amount = null, int? width = null,
            int? height = null, int? depth = null, bool? isFragile = null, int? cost = null, string? address = null);

        /// <summary>
        /// Deletes a package.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id);
    }
}
