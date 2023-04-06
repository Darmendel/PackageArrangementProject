namespace PackageArrangementServer.Models
{
    public class RequestEditPackage
    {
        //public string PackageId { get; }
        public string Type { get; set; }
        public string Amount { get; set; } // add type check
        public string Width { get; set; } // add type check
        public string Height { get; set; } // add type check
        public string Depth { get; set; } // add type check
        public string Weight { get; set; } // add type check
        //public bool IsFragile { get; set; }
        public string Cost { get; set; } // add type check
        public string Address { get; set; }
        //public string Server { get; set; }
    }
}
