using MongoDB.Bson.Serialization.Attributes;

namespace PackageArrangementServer.Models
{
    [BsonDiscriminator("BigContainer")]
    public class BigContainer : IContainer
    {
        const int HEIGHT = 800;
        const int WIDTH = 1000;
        const int LENGTH = 1800;
        const int COST = 1000;

        public BigContainer()
        {
            this.Height = HEIGHT.ToString();
            this.Width = WIDTH.ToString();
            this.Length = LENGTH.ToString();
            this.Cost = COST.ToString();
        }
    }
}
