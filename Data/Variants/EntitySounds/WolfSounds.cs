using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data.Variants.EntitySounds
{
    public sealed record WolfSounds : INbtSerializable<WolfSounds>
    {
        public static readonly Identifier RegistryName = "minecraft:wolf_sound_variant";

        public static readonly WolfSounds Stub = new()
        {
            Adult = WolfSoundAssets.Stub,
            Baby = WolfSoundAssets.Stub,
        };
        public static readonly Registry<WolfSounds> StubRegistry = new(RegistryName)
        {
            Entries =
            {
                [Identifier.Empty] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required WolfSoundAssets Adult { get; set; }
        public required WolfSoundAssets Baby { get; set; }

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