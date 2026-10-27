using DivaMC.Protocol.Registries;
using DivaMC.Protocol.World.Biomes;
using DivaMC.Protocol.World.Dimensions;

namespace DivaMC.Protocol.World.Chunks
{
    public sealed record ChunkContext
    {
        public required Registry<Biome> Biomes { get; set; }
        public required DimensionType Dimension { get; set; }
        
        public ChunkContext()
        {
            
        }
    }
}