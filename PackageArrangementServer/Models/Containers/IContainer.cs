using MongoDB.Bson.Serialization.Attributes;

namespace PackageArrangementServer.Models
{
    [BsonDiscriminator("IContainer")]
    [BsonKnownTypes(typeof(GeneralContainer), typeof(SmallContainer), typeof(MediumContainer), typeof(BigContainer))]
    public class IContainer
    {

        public string Height { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string Cost { get; set; }

        public bool Equals(IContainer other)
        {
            return Height.Equals(other.Height) && Width.Equals(other.Width) && Length.Equals(other.Length);
        }
    }
}
