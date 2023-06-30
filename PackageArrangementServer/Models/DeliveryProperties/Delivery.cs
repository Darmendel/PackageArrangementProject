using PackageArrangementServer.Services;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PackageArrangementServer.Models
{
    [BsonIgnoreExtraElements]
    public class Delivery 
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //[BsonElement("UserId")]
        public string UserId { get; set; } // define as second key
        public DateTime CreatedDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<Package> FirstPackages { get; set; }
        public List<Package> SecondPackages { get; set; }
        public IContainer? Container { get; set; }
        public string Cost { get; set; }
        public DeliveryStatus Status { get; set; }


        public Delivery(string id, string userId, DateTime? deliveryDate = null, List<Package> fpackages = null,
            List<Package> spackages = null, IContainer ? container = null)
        {
            this.Id = id;
            this.UserId = userId;
            this.CreatedDate = DateTime.Now;
            this.DeliveryDate = (DateTime) deliveryDate;
            this.FirstPackages = fpackages;
            this.SecondPackages = spackages;

            /*int cost = CalculateCost(packages, container);
            if (cost > 0) this.Cost = cost.ToString();
            else this.Cost = 0.ToString();*/

            this.Cost = 0.ToString();

            this.Container = (container != null) ? container : new MediumContainer();

            this.Status = DeliveryStatus.Pending;
        }

        /*private int CalculateCost(List<Package> packages = null, IContainer? container = null)
        {
            IDeliveryServiceHelper helper = new DeliveryServiceHelper();
            return helper.Cost(packages, container);
        }*/

    }
}
