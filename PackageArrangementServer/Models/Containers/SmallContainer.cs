using MongoDB.Bson.Serialization.Attributes;

namespace PackageArrangementServer.Models
{
    [BsonDiscriminator("SmallContainer")]
    public class SmallContainer : IContainer
    {
        const int HEIGHT = 400;
        const int WIDTH = 600;
        const int LENGTH = 1400;
        const int COST = 700;

        public SmallContainer()
        {
            this.Height = HEIGHT.ToString();
            this.Width = WIDTH.ToString();
            this.Length = LENGTH.ToString();
            this.Cost = COST.ToString();
        }
    }
}
