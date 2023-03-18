using System.ComponentModel.DataAnnotations;

namespace PackageArrangementServer.Models
{
    public class Delivery
    {
        [Key]
        public string id { get; set; }

        public string userId { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public List<Package> packages { get; set; }
        public Container? selectedContainer { get; set; }
        public int cost { get; set; }
        public string deliveryStatus { get; set; }


        public Delivery(string id, string userId)
        {
            this.id = id;
            this.userId = userId;
        }
    }
}
