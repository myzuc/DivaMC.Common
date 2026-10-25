using System.Collections.Immutable;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Objects.SpawnConditions
{
    public sealed record StructureSpawnCondition : SpawnCondition
    {
        public static readonly string Type = "structure";

        public required IImmutableList<Identifier> Structures { get; init; }
        
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