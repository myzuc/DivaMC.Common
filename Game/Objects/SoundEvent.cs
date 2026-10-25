using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Objects
{
    public sealed record SoundEvent : INbtSerializable<SoundEvent>
    {
        public static readonly SoundEvent None = Identifier.EmptySound;
        
        public Identifier Sound { get; }
        public float? Range { get; init; } = null;
        
        public SoundEvent(Identifier sound)
        {
            Sound = sound;
        }
        
        public static implicit operator SoundEvent(Identifier value)
        {
            return new(value);
        }
        public static implicit operator SoundEvent(string value)
        {
            return new(value);
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