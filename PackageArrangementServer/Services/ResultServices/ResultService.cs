using Newtonsoft.Json;
using PackageArrangementServer.Models;
using PackageArrangementServer.Models.Requests.RequestCreation;

namespace PackageArrangementServer.Services.ResultServices
{
    public class ResultService : IResultService
    {
        IUserService _userService;
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://localhost:5555"),
        };

        public ResultService(IUserService userService) { _userService = userService; }

        /// <Handles delivery arrangement>
        /// DeliveryArrangement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>string</returns>
        public string DeliveryArrangement(DeliveryTwoResults request)
        {
            /// Implement update delivery status
            /// Implement update delivery in db
            /// Implement send request to Application

            string result = null;
            string deliveryId = request.Id;
            string userId = request.UserId;

            if (!_userService.Exists(userId, "id")) return "This User " + userId + " does not exist";

            Delivery delivery = _userService.GetDelivery(userId, deliveryId);

            if (delivery != null)
            {
                delivery.firstPackages = request.FirstPackages;
                delivery.secondPackages = request.SecondPackages;
                delivery.Status = DeliveryStatus.Ready;
            } else result = "This delivery id: " + deliveryId + " does not exist";
            
            return result;
        }
    }
}
