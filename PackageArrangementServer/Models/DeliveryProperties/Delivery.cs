﻿using System.ComponentModel.DataAnnotations;

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
        public Container? Container { get; set; }
        public string Cost { get; set; } // add type check
        public DeliveryStatus Status { get; set; }


        public Delivery(string id, string userId, DateTime? deliveryDate = null, List<Package> packages = null,
            Container? container = null, string cost = null)
        {
            this.Id = id;
            this.UserId = userId;
            this.CreatedDate = DateTime.Now;
            this.DeliveryDate = (DateTime) deliveryDate;
            this.Packages = packages;
            this.Container = container;
            this.Cost = cost;
            this.Status = DeliveryStatus.Pending;
        }

        /**public Delivery(string id, string userId, DateTime deliveryDate, PackageList packages,
            Container container = null, string cost = null)
        {
            this(id, userId, deliveryDate, packages.Packages, container, cost);
        }*/
    }
}
