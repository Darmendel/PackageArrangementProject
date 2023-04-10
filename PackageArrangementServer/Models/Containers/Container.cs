namespace PackageArrangementServer.Models
{
    public struct Container
    {
        public SmallContainer? Small { get; }
        public MediumContainer? Medium { get; }
        public BigContainer? Big { get; }

        public Container(SmallContainer? s = null, MediumContainer? m = null, BigContainer? b = null)
        {
            Small = s;
            Medium = m;
            Big = b;
        }

    }
}
