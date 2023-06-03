using System.ComponentModel.DataAnnotations;

namespace PackageArrangementServer.Models
{
    public class Package
    {
        [Key]
        public string Id { get; set; }
        public string DeliveryId { get; set; } // define as second key
        public string Width { get; set; }
        public string Height { get; set; }
        public string Depth { get; set; }
        public string Order { get; set; }

        public Package(string id, string deliveryId, string width, string height,
            string depth, string order)
        {
            this.Id = id;
            this.DeliveryId = deliveryId;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
            this.Order = order;
        }

    }
}
