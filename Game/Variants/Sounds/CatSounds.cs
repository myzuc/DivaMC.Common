using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.Sounds
{
    public sealed record CatSounds : INbtSerializable<CatSounds>
    {
        public static readonly Identifier RegistryName = "minecraft:cat_sound_variant";

        public static readonly CatSounds Stub = new()
        {
            Adult = CatSoundAssets.Stub,
            Baby = CatSoundAssets.Stub,
        };
        public static readonly Registry<CatSounds> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [new(Identifier.Empty, Stub)]
        };
        
        public required CatSoundAssets Adult { get; init; }
        public required CatSoundAssets Baby { get; init; }

        public CatSounds()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Adult.ToNbt().Rename("adult_sounds").AddTo(nbt);
            Baby.ToNbt().Rename("baby_sounds").AddTo(nbt);
            return nbt;
        }
        public static CatSounds FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}