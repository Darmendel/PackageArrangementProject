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

            this.Cost = 0.ToString();

            this.Container = (container != null) ? container : new MediumContainer();

            this.Status = DeliveryStatus.Pending;
        }

    }
}
