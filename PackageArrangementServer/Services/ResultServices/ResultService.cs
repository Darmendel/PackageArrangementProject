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
        /// <returns>void</returns>
        public async void DeliveryArrangement(DeliveryTwoResults request)
        {
            /// Implement update delivery status
            /// Implement update delivery in db
            /// Implement send request to Application

            string deliveryId = request.Id;
            User user = _userService.Get(request.UserId, "id");
            if (user != null) {
                DeliveryRequest req = new DeliveryRequest(request.Id, request.Container, request.FirstPackages);
                req.UserId = user.Id;
                Console.WriteLine(request);
                StringContent message = new StringContent(JsonConvert.SerializeObject(request));
                HttpResponseMessage response = await sharedClient.PostAsync("delivery", message);
            }
            return;
        }
    }
}
