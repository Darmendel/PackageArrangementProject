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
        /// <returns>string</returns>
        public string DeliveryArrangement(DeliveryTwoResults request);
    }
}
