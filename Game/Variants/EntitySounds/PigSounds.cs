using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.EntitySounds
{
    public sealed record PigSounds : INbtSerializable<PigSounds>
    {
        public static readonly Identifier RegistryName = "minecraft:pig_sound_variant";

        public static readonly PigSounds Stub = new()
        {
            Adult = PigSoundAssets.Stub,
            Baby = PigSoundAssets.Stub,
        };
        public static readonly Registry<PigSounds> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [new(Identifier.Empty, Stub)]
        };
        
        public required PigSoundAssets Adult { get; init; }
        public required PigSoundAssets Baby { get; init; }

        public PigSounds()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Adult.ToNbt().Rename("adult_sounds").AddTo(nbt);
            Baby.ToNbt().Rename("baby_sounds").AddTo(nbt);
            return nbt;
        }
        public static PigSounds FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}