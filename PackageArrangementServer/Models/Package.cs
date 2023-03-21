using System.ComponentModel.DataAnnotations;

namespace PackageArrangementServer.Models
{
    public class Package
    {
        [Key]
        internal string Id { get; set; }
        internal string Type { get; set; }
        internal int Amount { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }
        internal int Depth { get; set; }
        internal int Weight { get; set; }
        internal bool IsFragile { get; set; }
        internal int Cost { get; set; }
        internal string Address { get; set; }

        public Package(string id, string type, int amount, int width, int height, int depth, int weight, bool isFragile, int cost, string address)
        {
            this.Id = id;
            this.Type = type;
            this.Amount = amount;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
            this.Weight = weight;
            this.IsFragile = isFragile;
            this.Cost = cost;
            this.Address = address;
        }

    }
}
