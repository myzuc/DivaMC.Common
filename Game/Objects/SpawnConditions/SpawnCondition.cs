using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Game.Objects.SpawnConditions
{
    public abstract record SpawnCondition : INbtSerializable<SpawnCondition>
    {
        private string Type { get; }

        internal SpawnCondition(string type)
        {
            Type = type;
        }

        public virtual NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            NbtString.FromT0A(Type).Rename("type").AddTo(nbt);
            return nbt;
        }
        public static SpawnCondition FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}