using DivaMC.Protocol.Data.Providers.Ranges;
using DivaMC.Protocol.IO.Serialization;
using fNbt;

namespace DivaMC.Protocol.Data.SpawnConditions
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