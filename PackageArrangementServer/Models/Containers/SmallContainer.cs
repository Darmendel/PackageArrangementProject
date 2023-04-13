namespace PackageArrangementServer.Models
{
    public struct SmallContainer : IContainer
    {
        public ContainerSize Size { get; }
        public string Height { get; }
        public string Width { get; }
        public string Depth { get; }
        public string Cost { get; }

        public SmallContainer()
        {
            this.Size = ContainerSize.Small;
            this.Height = 400.ToString();
            this.Width = 600.ToString();
            this.Depth = 1400.ToString();
            this.Cost = 700.ToString();
        }

        /*public bool IsSmall(IContainer container)
        {
            if (container == null) return false;
            if (container.GetType() == typeof(SmallContainer)) return true;
            return false;
        }*/
    }
}
