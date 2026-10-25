using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.EntitySounds
{
    public sealed record ChickenSounds : INbtSerializable<ChickenSounds>
    {
        public static readonly Identifier RegistryName = "minecraft:chicken_sound_variant";

        public static readonly ChickenSounds Stub = new()
        {
            Adult = ChickenSoundAssets.Stub,
            Baby = ChickenSoundAssets.Stub,
        };
        public static readonly Registry<ChickenSounds> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [new(Identifier.Empty, Stub)]
        };
        
        public required ChickenSoundAssets Adult { get; init; }
        public required ChickenSoundAssets Baby { get; init; }

        public ChickenSounds()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Adult.ToNbt().Rename("adult_sounds").AddTo(nbt);
            Baby.ToNbt().Rename("baby_sounds").AddTo(nbt);
            return nbt;
        }
        public static ChickenSounds FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}