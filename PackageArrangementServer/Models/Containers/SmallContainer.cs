using MongoDB.Bson.Serialization.Attributes;

namespace PackageArrangementServer.Models
{
    [BsonDiscriminator("SmallContainer")]
    public class SmallContainer : IContainer
    {
        public SmallContainer()
        {
            this.Height = 400.ToString();
            this.Width = 600.ToString();
            this.Length = 1400.ToString();
            this.Cost = 700.ToString();
        }

       
    }
}
