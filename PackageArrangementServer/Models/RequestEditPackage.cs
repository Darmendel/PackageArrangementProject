namespace PackageArrangementServer.Models
{
    public class RequestEditPackage
    {
        public string Type { get; set; }
        public int Amount { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }
        public int Weight { get; set; }
        public bool IsFragile { get; set; }
        public int Cost { get; set; }
        public string Address { get; set; }
        public string Server { get; set; }
    }
}
