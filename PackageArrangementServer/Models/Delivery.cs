using System.ComponentModel.DataAnnotations;

namespace PackageArrangementServer.Models
{
    public class Delivery
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<Package> Packages { get; set; }
        public Container? SelectedContainer { get; set; }
        public int Cost { get; set; }
        public string DeliveryStatus { get; set; }


        public Delivery(string id, string userId)
        {
            this.Id = id;
            this.UserId = userId;
        }
    }
}
