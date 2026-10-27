using System.Numerics;
using DivaMC.Protocol.IO.Data.PalettedContainer;
using DivaMC.Protocol.IO.Serialization;
using DivaMC.Protocol.World.Biomes;
using DivaMC.Protocol.World.Common;
using DivaMC.Protocol.World.Tiles;

namespace DivaMC.Protocol.World.Chunks
{
    public sealed class TileSection : IBinarySerializable<TileSection, ChunkContext>
    {
        public ChunkContext Context { get; }
        private short TileCount { get; set; } = 0;
        private short FluidCount { get; set; } = 0;
        private PalettedContainer Tiles { get; }
        private PalettedContainer Biomes { get; }
        
        public TileSection(ChunkContext context)
        {
            Context = context;
            Tiles = new(4096)
            {
                MinimumIndirectBits = 4,
                MaximumIndirectBits = 8,
                DirectBits = 15 //todo: get value based on present block states of version
            };
            Biomes = new(4096)
            {
                MinimumIndirectBits = 1,
                MaximumIndirectBits = 3,
                DirectBits = Context.Biomes.Entries.Count <= 0 ? 0 : BitOperations.Log2((uint)Context.Biomes.Entries.Count - 1) + 1
            };
        }

        public Tile this[SectionLocalLocation xyz]
        {
            get
            {
                return Tile.Unmap(Tiles[xyz.Index]);
            }
            set
            {
                Tile previous = this[xyz];
                if (previous.IsAir) TileCount++;
                if (previous.HasFluid) FluidCount++;
                Tiles[xyz.Index] = Tile.Map(value);
                if (value.IsAir) TileCount--;
                if (value.HasFluid) FluidCount--;
            }
        }
        public Tile this[int x, int y, int z]
        {
            get
            {
                return this[new(x, y, z)];
            }
            set
            {
                this[new(x, y, z)] = value;
            }
        }

        public void SetBiome(SectionLocalLocation xyz, Biome value)
        {
            Tiles[xyz.Index] = Context.Biomes.IndexOf(value);
        }
        public Biome GetBiome(SectionLocalLocation xyz)
        {
            return Context.Biomes.EntryOf(Tiles[xyz.Index]) ?? throw new InvalidDataException();
        }
        public void SetBiome(int x, int y, int z, Biome value)
        {
            SetBiome(new(x, y, z), value);
        }
        public Biome GetBiome(int x, int y, int z)
        {
            return GetBiome(new(x, y, z));
        }
        
        public void Serialize(Stream stream, ChunkContext context)
        {
            if (context != Context) throw new InvalidOperationException();
            stream.WriteS16(TileCount);
            stream.WriteS16(FluidCount);
            stream.Write(Tiles);
            stream.Write(Biomes);
        }
        public static TileSection Deserialize(Stream stream, ChunkContext context)
        {
            throw new NotImplementedException();
        }
    }
}