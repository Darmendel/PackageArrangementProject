namespace PackageArrangementServer.Models
{
    public class SmallContainer : IContainer
    {
        public ContainerSize Size { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Depth { get; set; }
        public string Cost { get; set; }

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
