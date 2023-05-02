namespace PackageArrangementServer.Models
{
    public class GeneralContainer : IContainer
    {
        public ContainerSize Size { get; }
        public string Height { get; }
        public string Width { get; }
        public string Depth { get; }
        public string Cost { get; }

        public GeneralContainer(string height, string width, string depth)
        {
            this.Size = ContainerSize.General;
            this.Height = height;
            this.Width = width;
            this.Depth = depth;
        }
    }
}
