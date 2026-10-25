using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game.Variants.EntitySounds
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
                ["Identifier.Empty"] = Stub
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