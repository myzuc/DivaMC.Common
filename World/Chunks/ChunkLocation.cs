using DivaMC.Protocol.IO.Serialization;

namespace DivaMC.Protocol.World.Chunks
{
    public readonly record struct ChunkLocation : IBinarySerializable<ChunkLocation>
    {
        public int X { get; } = 0;
        public int Z { get; } = 0;
        
        public ChunkLocation()
        {
            
        }
        public ChunkLocation(int x, int z)
        {
            X = x;
            Z = z;
        }
        
        public void Serialize(Stream stream)
        {
            stream.WriteS32(X);
            stream.WriteS32(Z);
        }
        public static ChunkLocation Deserialize(Stream stream)
        {
            return new(stream.ReadS32(), stream.ReadS32());
        }
    }
}