using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Data.SpawnConditions
{
    public sealed record SpawnConditionInfo : INbtSerializable<SpawnConditionInfo>
    {
        public SpawnCondition Condition { get; set; }
        public int Priority { get; set; }
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