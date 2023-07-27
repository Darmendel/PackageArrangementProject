using MongoDB.Bson.Serialization.Attributes;

namespace PackageArrangementServer.Models
{
    [BsonDiscriminator("MediumContainer")]
    public class MediumContainer : IContainer
    {
        const int HEIGHT = 600;
        const int WIDTH = 800;
        const int LENGTH = 1600;
        const int COST = 850;

        public MediumContainer()
        {
            this.Height = HEIGHT.ToString();
            this.Width = WIDTH.ToString();
            this.Length = LENGTH.ToString();
            this.Cost = COST.ToString();
        }
    }
}
