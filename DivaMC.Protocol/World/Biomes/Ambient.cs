using System.Drawing;
using DivaMC.Protocol.IO.Serialization;
using fNbt;

namespace DivaMC.Protocol.World.Biomes
{
    public sealed record Ambient : INbtSerializable<Ambient>
    {
        public required Color WaterColor { get; set; }
        public Color? FoliageColor { get; set; } = null;
        public Color? DryFoliageColor { get; set; } = null;
        public Color? GrassColor { get; set; } = null;
        public string? GrassColorModifier { get; set; } = null;
        
        public Ambient()
        {
            
        }
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            NbtInt.FromColor(WaterColor).Rename("water_color").AddTo(nbt); 
            if (FoliageColor.HasValue) NbtInt.FromColor(FoliageColor.Value).Rename("foliage_color").AddTo(nbt);
            if (DryFoliageColor.HasValue) NbtInt.FromColor(DryFoliageColor.Value).Rename("dry_foliage_color").AddTo(nbt);
            if (GrassColor.HasValue) NbtInt.FromColor(GrassColor.Value).Rename("grass_color").AddTo(nbt);
            if (GrassColorModifier is not null) NbtString.FromT0A(GrassColorModifier).Rename("grass_color_modifier").AddTo(nbt);
            return nbt;
        }
        public static Ambient FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}