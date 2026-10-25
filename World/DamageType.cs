using DivaMC.Common.Common;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Registries;
using fNbt;

namespace DivaMC.Common.World
{
    public sealed record DamageType : INbtSerializable<DamageType>
    {
        public static readonly Identifier RegistryName = "minecraft:damage_type";

        public static readonly DamageType Stub = new()
        {
            PartialTranslationKey = "generic",
            Exhaustion = 0.0f,
            Scaling = "never",
            Effects = null,
            Type = null
        };
        public static readonly Registry<DamageType> StubRegistry = new(RegistryName)
        {
            Entries = {
                ["minecraft:cactus"] = Stub,
                ["minecraft:campfire"] = Stub,
                ["minecraft:cramming"] = Stub,
                ["minecraft:dragon_breath"] = Stub,
                ["minecraft:drown"] = Stub,
                ["minecraft:dry_out"] = Stub,
                ["minecraft:ender_pearl"] = Stub,
                ["minecraft:fall"] = Stub,
                ["minecraft:fly_into_wall"] = Stub,
                ["minecraft:freeze"] = Stub,
                ["minecraft:generic"] = Stub,
                ["minecraft:generic_kill"] = Stub,
                ["minecraft:hot_floor"] = Stub,
                ["minecraft:in_fire"] = Stub,
                ["minecraft:in_wall"] = Stub,
                ["minecraft:lava"] = Stub,
                ["minecraft:lightning_bolt"] = Stub,
                ["minecraft:magic"] = Stub,
                ["minecraft:on_fire"] = Stub,
                ["minecraft:out_of_world"] = Stub,
                ["minecraft:outside_border"] = Stub,
                ["minecraft:spear"] = Stub,
                ["minecraft:stalagmite"] = Stub,
                ["minecraft:starve"] = Stub,
                ["minecraft:sweet_berry_bush"] = Stub,
                ["minecraft:wither"] = Stub,
            }
        };
        public static readonly RegistryTags StubRegistryTags = new(RegistryName)
        {
            Entries =
            {
                ["minecraft:bypasses_shield"] = [],
                ["minecraft:is_explosion"] = [],
                ["minecraft:is_fire"] = [],
            },
        };
        
        public required string PartialTranslationKey { get; set; }
        public required float Exhaustion { get; set; }
        public required string Scaling { get; set; }
        public required string? Effects { get; set; }
        public required string? Type { get; set; }
        
        public DamageType()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            NbtCompound nbt = new();
            NbtString.FromT0A(PartialTranslationKey).Rename("message_id").AddTo(nbt);
            NbtFloat.FromF32(Exhaustion).Rename("exhaustion").AddTo(nbt);
            NbtString.FromT0A(Scaling).Rename("scaling").AddTo(nbt);
            if (Effects is not null) NbtString.FromT0A(Effects).Rename("effects").AddTo(nbt);
            if (Type is not null) NbtString.FromT0A(Type).Rename("death_message_type").AddTo(nbt);
            return nbt;
        }
        public static DamageType FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}