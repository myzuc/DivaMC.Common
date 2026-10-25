using DivaMC.Common.Game.Objects;
using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Game.Variants.EntitySounds
{
    public sealed record ChickenSoundAssets : INbtSerializable<ChickenSoundAssets>
    {
        public static readonly ChickenSoundAssets Stub = new()
        {
            Ambient = SoundEvent.None,
            Step = SoundEvent.None,
            Hurt = SoundEvent.None,
            Death = SoundEvent.None
        };
        
        public required SoundEvent Ambient { get; set; }
        public required SoundEvent Step { get; set; }
        
        public required SoundEvent Hurt { get; set; }
        public required SoundEvent Death { get; set; }

        public ChickenSoundAssets()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Ambient.ToNbt().Rename("ambient_sound").AddTo(nbt);
            Step.ToNbt().Rename("step_sound").AddTo(nbt);
            Hurt.ToNbt().Rename("hurt_sound").AddTo(nbt);
            Death.ToNbt().Rename("death_sound").AddTo(nbt);
            return nbt;
        }
        public static ChickenSoundAssets FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}