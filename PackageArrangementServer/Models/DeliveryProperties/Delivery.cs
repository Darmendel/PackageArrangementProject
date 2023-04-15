using PackageArrangementServer.Services;
using System.ComponentModel.DataAnnotations;

namespace PackageArrangementServer.Models
{
    public class Delivery
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; } // define as second key
        public DateTime CreatedDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<Package> Packages { get; set; }
        public IContainer? Container { get; set; }
        public string Cost { get; set; }
        public DeliveryStatus Status { get; set; }


        public Delivery(string id, string userId, DateTime? deliveryDate = null, List<Package> packages = null,
            IContainer? container = null)
        {
            this.Id = id;
            this.UserId = userId;
            this.CreatedDate = DateTime.Now;
            this.DeliveryDate = (DateTime) deliveryDate;
            this.Packages = packages;

            /*int cost = CalculateCost(packages, container);
            if (cost > 0) this.Cost = cost.ToString();
            else this.Cost = 0.ToString();*/

            this.Cost = 0.ToString();

            if (container != null) this.Container = container;
            else this.Container = new MediumContainer();

            this.Status = DeliveryStatus.Pending;
        }

        /*private int CalculateCost(List<Package> packages = null, IContainer? container = null)
        {
            IDeliveryServiceHelper helper = new DeliveryServiceHelper();
            return helper.Cost(packages, container);
        }*/

    }
}
