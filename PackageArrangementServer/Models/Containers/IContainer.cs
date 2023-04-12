namespace PackageArrangementServer.Models
{
    public interface IContainer
    {
        public string Height { get; }
        public string Width { get; }
        public string Depth { get; }
        public string Cost { get; }
    }
}
