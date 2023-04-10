namespace PackageArrangementServer.Models
{
    public struct SmallContainer
    {
        public string Height { get; }
        public string Width { get; }
        public string Depth { get; }
        public string Cost { get; }

        public SmallContainer()
        {
            this.Height = 400.ToString();
            this.Width = 600.ToString();
            this.Depth = 1400.ToString();
            this.Cost = 700.ToString();
        }
    }
}
