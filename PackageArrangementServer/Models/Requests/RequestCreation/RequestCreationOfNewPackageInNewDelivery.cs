﻿namespace PackageArrangementServer.Models
{
    public class RequestCreationOfNewPackageInNewDelivery : IRequestCreation
    {
        public string Amount { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Depth { get; set; }
        public string Address { get; set; }
    }
}