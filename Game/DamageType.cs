using DivaMC.Common.Game.Registries;
using DivaMC.Common.IO.Serialization;
using DivaMC.Common.Primitives;
using fNbt;

namespace DivaMC.Common.Game
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
        public static readonly Registry<DamageType> StubRegistry = new()
        {
            Id = RegistryName,
            Entries = [
                new("minecraft:cactus", Stub),
                new("minecraft:campfire", Stub),
                new("minecraft:cramming", Stub),
                new("minecraft:dragon_breath", Stub),
                new("minecraft:drown", Stub),
                new("minecraft:dry_out", Stub),
                new("minecraft:ender_pearl", Stub),
                new("minecraft:fall", Stub),
                new("minecraft:fly_into_wall", Stub),
                new("minecraft:freeze", Stub),
                new("minecraft:generic", Stub),
                new("minecraft:generic_kill", Stub),
                new("minecraft:hot_floor", Stub),
                new("minecraft:in_fire", Stub),
                new("minecraft:in_wall", Stub),
                new("minecraft:lava", Stub),
                new("minecraft:lightning_bolt", Stub),
                new("minecraft:magic", Stub),
                new("minecraft:on_fire", Stub),
                new("minecraft:out_of_world", Stub),
                new("minecraft:outside_border", Stub),
                new("minecraft:spear", Stub),
                new("minecraft:stalagmite", Stub),
                new("minecraft:starve", Stub),
                new("minecraft:sweet_berry_bush", Stub),
                new("minecraft:wither", Stub),
            ]
        };
        public static readonly RegistryTagList StubRegistryTags = new()
        {
            Id = RegistryName,
            Entries =
            [
                new("minecraft:bypasses_shield", []),
                new("minecraft:is_explosion", []),
                new("minecraft:is_fire", []),
            ],
        };
        
        public required string PartialTranslationKey { get; init; }
        public required float Exhaustion { get; init; }
        public required string Scaling { get; init; }
        public required string? Effects { get; init; }
        public required string? Type { get; init; }
        
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