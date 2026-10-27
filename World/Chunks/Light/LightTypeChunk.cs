using DivaMC.Common.IO.Serialization;

namespace DivaMC.Common.World.Chunks.Light
{
    public sealed class LightTypeChunk : IBinarySerializable<LightChunk, ChunkContext>
    {
        public ChunkContext Context { get; }
        public int Sections { get; }
        public LightSection?[] Data { get; }
        public LightTypeChunk(ChunkContext context)
        {
            Context = context;
            Sections = DetermineSections(context.Dimension.Height);
            Data = new LightSection[Sections];
            for (int i = 0; i < Sections; i++)
            {
                Data[i] = null;
            }
        }

        public int this[int x, int y, int z]
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
        private LightSection GetSection(int y)
        {
            if (y < Context.Dimension.Depth || y > Context.Dimension.Height) throw new ArgumentOutOfRangeException();
            LightSection? section = Data[y / 16];
            if (section is null) throw new InvalidOperationException();
            return section;
        }
        
        public void Serialize(Stream stream, ChunkContext context)
        {
            if (Context == context) throw new InvalidOperationException();
            for (int i = 0; i < Sections; i++)
            {
                LightSection? section = Data[i];
                if (section is null) continue;
                section.Serialize(stream);
            }
        }
        public static LightChunk Deserialize(Stream stream, ChunkContext context)
        {
            throw new NotImplementedException();
        }
        
        private static int DetermineSections(int height)
        {
            if (height % 16 != 0) throw new ArgumentException();
            return height / 16 + 2;
        }
    }
}