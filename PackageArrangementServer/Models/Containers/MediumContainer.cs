namespace PackageArrangementServer.Models
{
    public struct MediumContainer : IContainer
    {
        public ContainerSize Size { get; }
        public string Height { get; }
        public string Width { get; }
        public string Depth { get; }
        public string Cost { get; }

        public MediumContainer()
        {
            this.Size = ContainerSize.Medium;
            this.Height = 600.ToString();
            this.Width = 800.ToString();
            this.Depth = 1600.ToString();
            this.Cost = 850.ToString();
        }

        public bool IsMedium(IContainer container)
        {
            if (container == null) return false;
            if (container.GetType() == typeof(MediumContainer)) return true;
            return false;
        }
    }
}
