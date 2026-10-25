using DivaMC.Common.Game.Objects;
using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Game.Variants.EntitySounds
{
    public sealed record WolfSoundAssets : INbtSerializable<WolfSoundAssets>
    {
        public static readonly WolfSoundAssets Stub = new()
        {
            Ambient = SoundEvent.None,
            Step = SoundEvent.None,
            Pant = SoundEvent.None,
            Whine = SoundEvent.None,
            Growl = SoundEvent.None,
            Hurt = SoundEvent.None,
            Death = SoundEvent.None
        };
        
        public required SoundEvent Ambient { get; init; }
        public required SoundEvent Step { get; init; }
        public required SoundEvent Pant { get; init; }
        
        public required SoundEvent Whine { get; init; }
        public required SoundEvent Growl { get; init; }
        public required SoundEvent Hurt { get; init; }
        public required SoundEvent Death { get; init; }

        public WolfSoundAssets()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Ambient.ToNbt().Rename("ambient_sound").AddTo(nbt);
            Step.ToNbt().Rename("step_sound").AddTo(nbt);
            Pant.ToNbt().Rename("pant_sound").AddTo(nbt);
            Whine.ToNbt().Rename("whine_sound").AddTo(nbt);
            Growl.ToNbt().Rename("growl_sound").AddTo(nbt);
            Hurt.ToNbt().Rename("hurt_sound").AddTo(nbt);
            Death.ToNbt().Rename("death_sound").AddTo(nbt);
            return nbt;
        }
        public static WolfSoundAssets FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}