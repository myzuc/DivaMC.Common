using DivaMC.Protocol.Common;
using DivaMC.Protocol.IO.Serialization;
using DivaMC.Protocol.Registries;
using fNbt;

namespace DivaMC.Protocol.Data.Variants.EntitySounds
{
    public sealed record ChickenSounds : INbtSerializable<ChickenSounds>
    {
        public static readonly Identifier RegistryName = "minecraft:chicken_sound_variant";

        public static readonly ChickenSounds Stub = new()
        {
            Adult = ChickenSoundAssets.Stub,
            Baby = ChickenSoundAssets.Stub,
        };
        public static readonly Registry<ChickenSounds> StubRegistry = new(RegistryName)
        {
            Entries =
            {
                [Identifier.Empty] = Stub
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required ChickenSoundAssets Adult { get; set; }
        public required ChickenSoundAssets Baby { get; set; }

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