using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data.Variants.EntitySounds
{
    public sealed record CowSounds : INbtSerializable<CowSounds>
    {
        public static readonly Identifier RegistryName = "minecraft:cow_sound_variant";

        public static readonly CowSounds Stub = new()
        {
            All = CowSoundAssets.Stub,
        };
        public static readonly Registry<CowSounds> StubRegistry = new(RegistryName)
        {
            Entries =
            {
                [Identifier.Empty] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required CowSoundAssets All { get; set; }

        public CowSounds()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            return All.ToNbt();
        }
        public static CowSounds FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}