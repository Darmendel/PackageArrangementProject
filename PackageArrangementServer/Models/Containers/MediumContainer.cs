namespace PackageArrangementServer.Models
{
    public class MediumContainer : IContainer
    {
        public string Height { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string Cost { get; set; }

        public MediumContainer()
        {
            this.Height = 600.ToString();
            this.Width = 800.ToString();
            this.Length = 1600.ToString();
            this.Cost = 850.ToString();
        }
    }
}
