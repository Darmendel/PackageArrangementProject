using System.ComponentModel.DataAnnotations;

namespace PackageArrangementServer.Models
{
    public class Package
    {
        [Key]
        public string Id { get; set; }
        public string DeliveryId { get; set; } // define as second key
        // public string Type { get; set; }
        public string Amount { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Depth { get; set; }
        public string Address { get; set; }

        public Package(string id, string deliveryId, string amount, string width, string height,
            string depth, string address)
        {
            this.Id = id;
            this.DeliveryId = deliveryId;
            // this.Type = type;
            this.Amount = amount;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
            this.Address = address;
        }

    }
}
