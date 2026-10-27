using DivaMC.Common.Registries;
using DivaMC.Common.World.Biomes;
using DivaMC.Common.World.Chunks;
using DivaMC.Common.World.Dimensions;

namespace DivaMC.Common.IO.Protocol.Packets
{
    public sealed record ConnectionContext
    {
        public required Registry<DimensionType> Dimensions { get; set; }
        public required Registry<Biome> Biomes { get; set; }
        public DimensionType? Dimension { get; set; } = null;
        
        public ConnectionContext()
        {
            
        }

        internal ChunkContext DeriveChunkContext()
        {
            if (Dimension is null) throw new InvalidOperationException();
            return new()
            {
                Biomes = Biomes,
                Dimension = Dimension,
            };
        }
    }
}