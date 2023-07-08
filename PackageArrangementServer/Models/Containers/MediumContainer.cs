using MongoDB.Bson.Serialization.Attributes;

namespace PackageArrangementServer.Models
{
    [BsonDiscriminator("MediumContainer")]
    public class MediumContainer : IContainer
    {
        public MediumContainer()
        {
            this.Height = 600.ToString();
            this.Width = 800.ToString();
            this.Length = 1600.ToString();
            this.Cost = 850.ToString();
        }
    }
}
