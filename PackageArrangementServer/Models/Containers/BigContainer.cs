namespace PackageArrangementServer.Models
{
    public class BigContainer : IContainer
    {
        public BigContainer()
        {
            this.Height = 800.ToString();
            this.Width = 1000.ToString();
            this.Length = 1800.ToString();
            this.Cost = 1000.ToString();
        }
    }
}
