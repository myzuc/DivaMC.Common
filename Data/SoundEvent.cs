using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Data
{
    public sealed record SoundEvent : INbtSerializable<SoundEvent>
    {
        public static readonly SoundEvent None = Identifier.EmptySound;
        
        public required Identifier Sound { get; set; }
        public float? Range { get; set; } = null;
        
        public SoundEvent()
        {
            
        }
        
        public static implicit operator SoundEvent(Identifier value)
        {
            return new()
            {
                Sound = value
            };
        }
        public static implicit operator SoundEvent(string value)
        {
            return new()
            {
                Sound = value
            };
        }
        
        public NbtTag ToNbt()
        {
            if (!Range.HasValue) return NbtString.FromT0A(Sound);
            NbtCompound nbt = new();
            NbtString.FromT0A(Sound).Rename("sound_id").AddTo(nbt);
            NbtFloat.FromF32(Range.Value).Rename("range").AddTo(nbt);
            return nbt;
        }
        public static SoundEvent FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}