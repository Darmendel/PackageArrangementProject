namespace PackageArrangementServer.Models
{
    public class BigContainer : IContainer
    {
        public ContainerSize Size { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Depth { get; set; }
        public string Cost { get; set; }

        public BigContainer()
        {
            this.Size = ContainerSize.Large;
            this.Height = 800.ToString();
            this.Width = 1000.ToString();
            this.Depth = 1800.ToString();
            this.Cost = 1000.ToString();
        }

        /*public bool IsBig(IContainer container)
        {
            if (container == null) return false;
            if (container.GetType() == typeof(BigContainer)) return true;
            return false;
        }*/
    }
}
