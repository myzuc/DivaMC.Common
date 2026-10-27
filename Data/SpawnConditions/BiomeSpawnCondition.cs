using DivaMC.Protocol.Common;
using DivaMC.Protocol.IO.Serialization;
using fNbt;

namespace DivaMC.Protocol.Data.SpawnConditions
{
    public sealed record BiomeSpawnCondition : SpawnCondition
    {
        public static readonly string Type = "biome";

        public required List<Identifier> Biomes { get; set; }
        
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