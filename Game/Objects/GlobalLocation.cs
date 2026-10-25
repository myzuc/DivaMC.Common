using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;

namespace DivaMC.Common.Game.Objects
{
    public sealed record GlobalLocation : IBinarySerializable<GlobalLocation>
    {
        public required Identifier Dimension { get; init; }
        public required Location Location { get; init; }
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