using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Game.Objects.SpawnConditions
{
    public sealed record SpawnConditionInfo : INbtSerializable<SpawnConditionInfo>
    {
        public SpawnCondition Condition { get; init; }
        public int Priority { get; init; }
        public SpawnConditionInfo(SpawnCondition condition, int priority)
        {
            Condition = condition;
            Priority = priority;
        }
        
        NbtTag INbtSerializable<SpawnConditionInfo>.ToNbt()
        {
            NbtCompound nbt = new();
            NbtInt.FromS32(Priority).Rename("priority").AddTo(nbt);
            Condition.ToNbt().Rename("condition").AddTo(nbt);
            return nbt;
        }
        static SpawnConditionInfo INbtSerializable<SpawnConditionInfo>.FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}