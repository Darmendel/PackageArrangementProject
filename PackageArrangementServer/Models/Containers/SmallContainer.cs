namespace PackageArrangementServer.Models
{
    public class SmallContainer : IContainer
    {
        public string Height { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string Cost { get; set; }

        public SmallContainer()
        {
            this.Height = 400.ToString();
            this.Width = 600.ToString();
            this.Length = 1400.ToString();
            this.Cost = 700.ToString();
        }

       
    }
}
