namespace PackageArrangementServer.Models.Containers
{
    public class NoCostContainer
    {
        public string Height { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }

        public NoCostContainer(string height, string width, string length)
        {
            this.Height = height;
            this.Width = width;
            this.Length = length;
        }
    }
}
