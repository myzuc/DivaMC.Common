using System.Collections.Immutable;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Objects.SpawnConditions
{
    public sealed record BiomeSpawnCondition : SpawnCondition
    {
        public static readonly string Type = "biome";

        public required IImmutableList<Identifier> Biomes { get; init; }
        
        public BiomeSpawnCondition() : base(Type)
        {
            
        }
        
        public override NbtTag ToNbt()
        {
            NbtCompound nbt = (NbtCompound)base.ToNbt();
            NbtString.FromT0A(Type).Rename("type").AddTo(nbt);
            NbtList.FromIEnumerable(Biomes).Rename("biomes").AddTo(nbt);
            return nbt;
        }
    }
}