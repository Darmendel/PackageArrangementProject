using PackageArrangementServer.Models;
using PackageArrangementServer.Models.Requests.RequestCreation;

namespace PackageArrangementServer.Services.ResultServices
{
    public interface IResultService
    {
        /// <Handles delivery arrangement>
        /// DeliveryArrangement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>void</returns>
        public void DeliveryArrangement(DeliveryTwoResults request);
    }
}
