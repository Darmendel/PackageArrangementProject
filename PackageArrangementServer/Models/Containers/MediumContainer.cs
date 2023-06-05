namespace PackageArrangementServer.Models
{
    public class MediumContainer : IContainer
    {
        public ContainerSize Size { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Depth { get; set; }
        public string Cost { get; set; }

        public MediumContainer()
        {
            this.Size = ContainerSize.Medium;
            this.Height = 600.ToString();
            this.Width = 800.ToString();
            this.Depth = 1600.ToString();
            this.Cost = 850.ToString();
        }

        /*public bool IsMedium(IContainer container)
        {
            if (container == null) return false;
            if (container.GetType() == typeof(MediumContainer)) return true;
            return false;
        }*/
    }
}
