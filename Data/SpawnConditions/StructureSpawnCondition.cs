using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Data.SpawnConditions
{
    public sealed record StructureSpawnCondition : SpawnCondition
    {
        public static readonly string Type = "structure";

        public required List<Identifier> Structures { get; set; }
        
        public StructureSpawnCondition() : base(Type)
        {
            
        }
        
        public override NbtTag ToNbt()
        {
            NbtCompound nbt = (NbtCompound)base.ToNbt();
            NbtList.FromIEnumerable(Structures).Rename("structures").AddTo(nbt);
            return nbt;
        }
    }
}