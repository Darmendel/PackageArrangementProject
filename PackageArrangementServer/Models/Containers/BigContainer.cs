using PackageArrangementServer.Models.Containers;

namespace PackageArrangementServer.Models
{
    public struct BigContainer : IContainer
    {
        public string Height { get; }
        public string Width { get; }
        public string Depth { get; }
        public string Cost { get; }

        public BigContainer()
        {
            this.Height = 800.ToString();
            this.Width = 1000.ToString();
            this.Depth = 1800.ToString();
            this.Cost = 1000.ToString();
        }

        public bool IsBig(IContainer container)
        {
            if (container == null) return false;
            if (container.GetType() == typeof(BigContainer)) return true;
            return false;
        }
    }
}
