using System.Numerics;
using DivaMC.Common.IO.Data;
using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.World.Chunks.Heightmaps
{
    public sealed class Heightmap : IBinarySerializable<Heightmap, ChunkContext>
    {
        public ChunkContext Context { get; }
        public HeightmapType Type { get; }
        public PackedArray Data { get; }
        
        public Heightmap(ChunkContext context, HeightmapType type)
        {
            Context = context;
            Type = type;
            Data = new(DetermineHeightBits(Context.Dimension.Height), 256);
        }
        public Heightmap(IList<int> data, ChunkContext context, HeightmapType type) : this(context, type)
        {
            if (data.Count < 256) throw new ArgumentException();
            for (int i = 0; i < 256; i++) Data[i] = data[i];
        }
        public Heightmap(PackedArray data, ChunkContext context, HeightmapType type) : this(context, type)
        {
            if (data.Length != 256 || data.Bits != Data.Bits) throw new ArgumentException();
            data.CopyTo(Data);
        }
            
        public int this[int x, int z]
        {
            get
            {
                if (x < 0 | x >= 16 ) throw new ArgumentOutOfRangeException();
                if (z < 0 | z >= 16) throw new ArgumentOutOfRangeException();
                return (int)Data[x | (z << 4)];
            }
            set
            {
                if (x < 0 | x >= 16 ) throw new ArgumentOutOfRangeException();
                if (z < 0 | z >= 16) throw new ArgumentOutOfRangeException();
                if (value < 0 | value > Context.Dimension.Height) throw new ArgumentOutOfRangeException();
                Data[x | (z << 4)] = value;
            }
        }
        
        public void Serialize(Stream stream, ChunkContext context)
        {
            if (context != Context) throw new InvalidOperationException();
            stream.WriteVS32((int)Type);
            stream.Write(Data, (Data.Bits, Data.Length));
        }
        public static Heightmap Deserialize(Stream stream, ChunkContext context)
        {
            HeightmapType type = stream.ReadVS32<HeightmapType>();
            PackedArray data = stream.Read<PackedArray, (int bits, int length)>((DetermineHeightBits(context.Dimension.Height), 256));
            return new(data, context, type);
        }

        private static int DetermineHeightBits(int height)
        {
            return BitOperations.Log2((uint)height) + 1;
        }
    }
}