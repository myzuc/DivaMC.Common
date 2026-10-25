using DivaMC.Common.Game.Providers.Ranges;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Objects.SpawnConditions
{
    public sealed record MoonBrightnessSpawnCondition : SpawnCondition
    {
        public static readonly string Type = "moon_brightness";

        public required DoubleRange Range { get; set; }
        
        public MoonBrightnessSpawnCondition() : base(Type)
        {
            
        }
        
        public override NbtTag ToNbt()
        {
            NbtCompound nbt = (NbtCompound)base.ToNbt();
            Range.ToNbt().Rename("range").AddTo(nbt);
            return nbt;
        }
    }
}