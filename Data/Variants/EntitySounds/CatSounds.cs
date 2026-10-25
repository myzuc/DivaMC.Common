using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data.Variants.EntitySounds
{
    public sealed record CatSounds : INbtSerializable<CatSounds>
    {
        public static readonly Identifier RegistryName = "minecraft:cat_sound_variant";

        public static readonly CatSounds Stub = new()
        {
            Adult = CatSoundAssets.Stub,
            Baby = CatSoundAssets.Stub,
        };
        public static readonly Registry<CatSounds> StubRegistry = new(RegistryName)
        {
            Entries =
            {
                ["Identifier.Empty"] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required CatSoundAssets Adult { get; set; }
        public required CatSoundAssets Baby { get; set; }

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