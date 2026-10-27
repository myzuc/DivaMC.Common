using DivaMC.Protocol.Registries;
using DivaMC.Protocol.World.Biomes;
using DivaMC.Protocol.World.Chunks;
using DivaMC.Protocol.World.Dimensions;

namespace DivaMC.Protocol.IO.Protocol
{
    public sealed record ConnectionContext
    {
        public required Registry<Dimension> Dimensions { get; set; }
        public required Registry<Biome> Biomes { get; set; }
        public Dimension? Dimension { get; set; } = null;
        
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