namespace PackageArrangementServer.Models
{
    public class GeneralContainer : IContainer
    {
        public string Height { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string Cost { get; set; }

        public GeneralContainer(string height, string width, string length)
        {
            this.Height = height;
            this.Width = width;
            this.Length = length;
            this.Cost = (int.Parse(Height) * int.Parse(Length) * int.Parse(Width)).ToString();
        }
    }
}
