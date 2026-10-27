using DivaMC.Common.IO.Serialization;
using DivaMC.Common.World.Biomes;
using DivaMC.Common.World.Tiles;

namespace DivaMC.Common.World.Chunks
{
    public sealed class TileChunk : IBinarySerializable<TileChunk, ChunkContext>
    {
        public ChunkContext Context { get; }
        public int Sections { get; }
        public TileSection[] Data { get; }
        
        public TileChunk(ChunkContext context)
        {
            Context = context;
            Sections = DetermineSections(context.Dimension.Height);
            Data = new TileSection[Sections];
            for (int i = 0; i < Sections; i++)
            {
                Data[i] = new(context);
            }
        }
        
        public Tile this[int x, int y, int z]
        {
            get
            {
                return GetSection(y)[x, y % 16, z];
            }
            set
            {
                GetSection(y)[x, y % 16, z] = value;
            }
        }
        public void SetBiome(int x, int y, int z, Biome value)
        {
            GetSection(y).SetBiome(x, y % 15, z, value);
        }
        public Biome GetBiome(int x, int y, int z)
        {
            return GetSection(y).GetBiome(x, y % 15, z);
        }
        
        private TileSection GetSection(int y)
        {
            if (y < Context.Dimension.Depth || y > Context.Dimension.Height) throw new ArgumentOutOfRangeException();
            TileSection? section = Data[y / 16 - Context.Dimension.Depth / 16];
            if (section is null) throw new InvalidOperationException();
            return section;
        }
        
        public void Serialize(Stream stream, ChunkContext context)
        {
            if (context != Context) throw new InvalidOperationException();
            for (int i = 0; i < Sections; i++)
            {
                stream.Write(Data[i], context);
            }
        }
        public static TileChunk Deserialize(Stream stream, ChunkContext context)
        {
            throw new NotImplementedException();
        }

        public byte[] ToArray(ChunkContext context)
        {
            using MemoryStream ms = new();
            ms.Write(this, context);
            return ms.ToArray();
        }
        public static TileChunk FromArray(byte[] data, ChunkContext context)
        {
            using MemoryStream ms = new(data);
            return Deserialize(ms, context);
        }
        
        private static int DetermineSections(int height)
        {
            if (height % 16 != 0) throw new ArgumentException();
            return height / 16;
        }
    }
}