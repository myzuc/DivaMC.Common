using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.EntitySounds
{
    public sealed record WolfSounds : INbtSerializable<WolfSounds>
    {
        public static readonly Identifier RegistryName = "minecraft:wolf_sound_variant";

        public static readonly WolfSounds Stub = new()
        {
            Adult = WolfSoundAssets.Stub,
            Baby = WolfSoundAssets.Stub,
        };
        public static readonly Registry<WolfSounds> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [new(Identifier.Empty, Stub)]
        };
        
        public required WolfSoundAssets Adult { get; init; }
        public required WolfSoundAssets Baby { get; init; }

        public WolfSounds()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Adult.ToNbt().Rename("adult_sounds").AddTo(nbt);
            Baby.ToNbt().Rename("baby_sounds").AddTo(nbt);
            return nbt;
        }
        public static WolfSounds FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}