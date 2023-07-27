using Newtonsoft.Json;
using PackageArrangementServer.Models;
using PackageArrangementServer.Models.Requests.RequestCreation;

namespace PackageArrangementServer.Services.ResultServices
{
    public class ResultService : IResultService
    {
        IUserService _userService;
        IDeliveryService _deliveryService;
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://localhost:5555"),
        };

        public ResultService(IUserService userService, IDeliveryService deliveryService) 
        { _userService = userService; _deliveryService = deliveryService; }

        /// <Handles delivery arrangement>
        /// DeliveryArrangement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>string</returns>
        public string DeliveryArrangement(DeliveryTwoResults request)
        {
            string result = null;
            string deliveryId = request.Id;
            string userId = request.UserId;

            if (!_userService.Exists(userId, "id")) return "This User " + userId + " does not exist";

            Delivery delivery = _userService.GetDelivery(userId, deliveryId);

            if (delivery != null)
            {
                delivery.FirstPackages = request.FirstPackages;
                delivery.SecondPackages = request.SecondPackages;
                delivery.Status = DeliveryStatus.Ready;
                _deliveryService.Update(deliveryId, delivery);
            } else result = "This delivery id: " + deliveryId + " does not exist";
            
            return result;
        }
    }
}
