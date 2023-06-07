namespace PackageArrangementServer.Models
{
    public class GeneralContainer : IContainer
    {
        public ContainerSize Size { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Depth { get; set; }
        public string Cost { get; set; }

        public GeneralContainer(string height, string width, string depth)
        {
            this.Size = ContainerSize.General;
            this.Height = height;
            this.Width = width;
            this.Depth = depth;
        }
    }
}
