using PackageArrangementServer.Models.Containers;
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
        public IContainer? Container { get; set; }
        public string Cost { get; set; } // add type check
        public DeliveryStatus Status { get; set; }


        public Delivery(string id, string userId, DateTime? deliveryDate = null, List<Package> packages = null,
            IContainer? container = null)
        {
            this.Id = id;
            this.UserId = userId;
            this.CreatedDate = DateTime.Now;
            this.DeliveryDate = (DateTime) deliveryDate;
            this.Packages = packages;
            this.Container = container;

            //if (container != null) this.Cost = CalculateCost(this).ToString();
            if (container != null) this.Cost = this.Container.Cost;
            else this.Cost = 0.ToString();

            this.Status = DeliveryStatus.Pending;
        }

        /*public int CalculateCost(Delivery delivery)
        {
            if (delivery == null) return -1;

            if (delivery.Packages == null && delivery.Container == null) return -1;

            int cost = 0;

            if (delivery.Packages != null)
            {
                foreach (Package package in delivery.Packages)
                {
                    if (package.Cost == null) continue;

                    try
                    {
                        int pc = Int32.Parse(package.Cost);
                        cost += pc;
                    }
                    catch (FormatException) { }
                }
            }

            if (delivery.Container != null)
            {
                try
                {
                    int c = Int32.Parse(delivery.Container.Cost);
                    cost += c;
                }
                catch (FormatException) { }
            }

            return cost;
        }*/
    }
}
