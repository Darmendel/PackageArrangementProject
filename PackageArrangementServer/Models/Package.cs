using System.ComponentModel.DataAnnotations;

namespace PackageArrangementServer.Models
{
    public class Package
    {
        [Key]
        internal string id { get; set; }
        internal string type { get; set; }
        internal int amount { get; set; }
        internal int width { get; set; }
        internal int height { get; set; }
        internal int depth { get; set; }
        internal int weight { get; set; }
        internal bool isFragile { get; set; }
        internal int cost { get; set; }
        internal string address { get; set; }

        public Package(string id, string type, int amount, int width, int height, int depth, int weight, bool isFragile, int cost, string address)
        {
            this.id = id;
            this.type = type;
            this.amount = amount;
            this.width = width;
            this.height = height;
            this.depth = depth;
            this.weight = weight;
            this.isFragile = isFragile;
            this.cost = cost;
            this.address = address;
        }

    }
}
