using DivaMC.Protocol.Common;
using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.World.Common.Objects
{
    public sealed record GlobalLocation : IByteSerializable<GlobalLocation>
    {
        public required Identifier Dimension { get; set; }
        public required Location Location { get; set; }
        public GlobalLocation()
        {
            
        }
        
        public void Serialize(Stream stream)
        {
            stream.Write(Dimension);
            stream.Write(Location);
        }
        public static GlobalLocation Deserialize(Stream stream)
        {
            return new()
            {
                Dimension = stream.Read<Identifier>(),
                Location = stream.Read<Location>(),
            };
        }
    }
}