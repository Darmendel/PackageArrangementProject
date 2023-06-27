using MongoDB.Bson.Serialization.Attributes;

namespace PackageArrangementServer.Models
{
    [BsonDiscriminator("GeneralContainer")]
    public class GeneralContainer : IContainer
    {
        public GeneralContainer(string height, string width, string length)
        {
            this.Height = height;
            this.Width = width;
            this.Length = length;
            this.Cost = (int.Parse(Height) * int.Parse(Length) * int.Parse(Width)).ToString();
        }
    }
}
