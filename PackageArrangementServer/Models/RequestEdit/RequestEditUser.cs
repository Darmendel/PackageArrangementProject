﻿namespace PackageArrangementServer.Models.RequestEdit
{
    public class RequestEditUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Delivery> Deliveries { get; set; }
    }
}
