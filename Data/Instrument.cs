using DivaMC.Common.Chat.Components;
using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.Data
{
    public sealed record Instrument : INbtSerializable<Instrument>
    {
        public static readonly Identifier RegistryName = "minecraft:instrument";

        public static readonly Instrument Stub = new()
        {
            Description = string.Empty,
            Sound = SoundEvent.None,
            Range = 1.0f,
            Cooldown = TimeSpan.FromSeconds(1),
            ItemDamage = 0,
        };
        public static readonly Registry<Instrument> StubRegistry = new(RegistryName)
        {
            Entries = {
                ["minecraft:ponder_goat_horn"] = Stub,
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName);
        
        public required ChatComponent Description { get; set; }
        public required SoundEvent Sound { get; set; }
        public required float Range { get; set; }
        public required TimeSpan Cooldown { get; set; }
        public required int ItemDamage { get; set; }
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            Description.ToNbt().Rename("description").AddTo(nbt);
            Sound.ToNbt().Rename("sound_event").AddTo(nbt);
            NbtFloat.FromF32(Range).Rename("range").AddTo(nbt);
            NbtFloat.FromF32((float)Cooldown.TotalSeconds).Rename("use_duration").AddTo(nbt);
            NbtInt.FromS32(ItemDamage).Rename("durability_damage").AddTo(nbt);
            return nbt;
        }
        public static Instrument FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}