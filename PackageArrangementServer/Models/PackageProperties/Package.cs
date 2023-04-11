using System.ComponentModel.DataAnnotations;

namespace PackageArrangementServer.Models
{
    public class Package
    {
        [Key]
        public string Id { get; set; }
        public string DeliveryId { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }
        public string Width { get; set; } // add type check
        public string Height { get; set; } // add type check
        public string Depth { get; set; } // add type check
        public string Weight { get; set; } // add type check
        //public bool IsFragile { get; set; }
        public string Cost { get; set; } // add type check
        public string Address { get; set; }

        public Package(string id, string deliveryId, string type, string amount, string width, string height,
            string depth, string weight, string cost, string address)
        {
            this.Id = id;
            this.DeliveryId = deliveryId;
            this.Type = type;
            this.Amount = amount;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
            this.Weight = weight;
            //this.IsFragile = isFragile;
            this.Cost = cost;
            this.Address = address;
        }

    }
}
