﻿using System.ComponentModel.DataAnnotations;

namespace PackageArrangementServer.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Delivery> Deliveries { get; set; }
    }
}
