using DivaMC.Common.Game.Objects;
using DivaMC.Common.IO.Serialization;
using fNbt;

namespace DivaMC.Common.Game.Variants.Sounds
{
    public sealed record PigSoundAssets : INbtSerializable<PigSoundAssets>
    {
        public static readonly PigSoundAssets Stub = new()
        {
            Ambient = SoundEvent.None,
            Step = SoundEvent.None,
            Eat = SoundEvent.None,
            Hurt = SoundEvent.None,
            Death = SoundEvent.None
        };
        
        public required SoundEvent Ambient { get; init; }
        public required SoundEvent Step { get; init; }
        
        public required SoundEvent Eat { get; init; }
        
        public required SoundEvent Hurt { get; init; }
        public required SoundEvent Death { get; init; }

        public PigSoundAssets()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Ambient.ToNbt().Rename("ambient_sound").AddTo(nbt);
            Step.ToNbt().Rename("step_sound").AddTo(nbt);
            Eat.ToNbt().Rename("eat_sound").AddTo(nbt);
            Hurt.ToNbt().Rename("hurt_sound").AddTo(nbt);
            Death.ToNbt().Rename("death_sound").AddTo(nbt);
            return nbt;
        }
        public static PigSoundAssets FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}