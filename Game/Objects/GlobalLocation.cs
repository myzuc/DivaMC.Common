using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;

namespace DivaMC.Common.Game.Objects
{
    public sealed record GlobalLocation : IBinarySerializable<GlobalLocation>
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