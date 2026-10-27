using DivaMC.Common.Registries;
using DivaMC.Common.World.Biomes;
using DivaMC.Common.World.Dimensions;

namespace DivaMC.Common.World.Chunks
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